import { act, render, screen, waitFor } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { afterEach, describe, expect, test, vi } from "vitest";

import { Timeline } from "./Timeline";

const chronoMock = vi.fn(
  ({ mode, items }: { mode: string; items: unknown[] }) => (
    <div data-testid="chrono" data-mode={mode} data-items-length={items.length} />
  ),
);

vi.mock("react-chrono", () => ({
  Chrono: (props: { mode: string; items: unknown[] }) => chronoMock(props),
}));

const mockEvents = [
  {
    title: "First Gate Ignites",
    description: "Dr. Lysa Quell stabilizes the experimental Aether Gate.",
    date: "2072-03-14",
  },
  {
    title: "Treaty of Twin Suns",
    description: "Human envoys broker a peace accord with the Virelian Houses.",
    date: "2074-08-29",
  },
];

type MatchMediaController = {
  update(matches: boolean): void;
};

const setupMatchMedia = (matches: boolean): MatchMediaController => {
  const listeners = new Set<(event: MediaQueryListEvent) => void>();

  const mediaQueryList: MediaQueryList = {
    matches,
    media: "(max-width: 640px)",
    onchange: null,
    addEventListener: vi.fn((_, listener: EventListenerOrEventListenerObject) => {
      if (typeof listener === "function") {
        listeners.add(listener as (event: MediaQueryListEvent) => void);
      }
    }),
    removeEventListener: vi.fn((_, listener: EventListenerOrEventListenerObject) => {
      if (typeof listener === "function") {
        listeners.delete(listener as (event: MediaQueryListEvent) => void);
      }
    }),
    dispatchEvent: vi.fn(),
    addListener: vi.fn(),
    removeListener: vi.fn(),
  } as MediaQueryList;

  if (typeof window.matchMedia !== "function") {
    Object.defineProperty(window, "matchMedia", {
      writable: true,
      configurable: true,
      value: vi.fn(),
    });
  }

  vi.spyOn(window, "matchMedia").mockImplementation(() => mediaQueryList);

  return {
    update(nextMatch: boolean) {
      mediaQueryList.matches = nextMatch;
      const event = { matches: nextMatch, media: mediaQueryList.media } as MediaQueryListEvent;
      listeners.forEach((listener) => listener(event));
    },
  };
};

describe("Timeline", () => {
  afterEach(() => {
    vi.restoreAllMocks();
  });

  test("renders timeline data and defaults to horizontal on large screens", () => {
    setupMatchMedia(false);

    render(<Timeline events={mockEvents} initialOrientation="horizontal" accentColor="#38bdf8" />);

    expect(screen.getByText(/timeline orientation/i)).toBeInTheDocument();

    const container = screen.getByTestId("timeline-container");
    expect(container).toHaveAttribute("data-orientation", "horizontal");

    const chronoNode = screen.getByTestId("chrono");
    expect(chronoNode).toHaveAttribute("data-mode", "HORIZONTAL");
    expect(chronoNode).toHaveAttribute("data-items-length", mockEvents.length.toString());
  });

  test("allows the user to toggle between orientations", async () => {
    setupMatchMedia(false);
    const user = userEvent.setup();

    render(<Timeline events={mockEvents} initialOrientation="horizontal" accentColor="#38bdf8" />);

    const container = screen.getByTestId("timeline-container");
    const verticalButton = screen.getByRole("button", { name: /vertical/i });
    const horizontalButton = screen.getByRole("button", { name: /horizontal/i });

    expect(container).toHaveAttribute("data-orientation", "horizontal");

    await user.click(verticalButton);
    expect(container).toHaveAttribute("data-orientation", "vertical");

    await user.click(horizontalButton);
    expect(container).toHaveAttribute("data-orientation", "horizontal");
  });

  test("prefers vertical orientation on small screens", async () => {
    const controller = setupMatchMedia(true);

    render(<Timeline events={mockEvents} initialOrientation="horizontal" accentColor="#38bdf8" />);

    const container = screen.getByTestId("timeline-container");
    expect(container).toHaveAttribute("data-orientation", "vertical");

    await act(async () => {
      controller.update(false);
    });

    await waitFor(() => {
      expect(container).toHaveAttribute("data-orientation", "horizontal");
    });
  });
});
