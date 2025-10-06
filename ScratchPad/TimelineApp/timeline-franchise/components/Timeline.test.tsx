import React from "react";
import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { vi } from "vitest";

const chronoMock = vi.hoisted(() => ({
  Chrono: ({
    items,
    mode,
  }: {
    items: Array<{ cardTitle: string; cardDetailedText: string }>;
    mode: string;
  }) => (
    <div data-testid="chrono-mock" data-mode={mode}>
      {items.map((item) => (
        <article key={item.cardTitle}>
          <h3>{item.cardTitle}</h3>
          <p>{item.cardDetailedText}</p>
        </article>
      ))}
    </div>
  ),
}));

vi.mock("react-chrono", () => chronoMock);

import { Timeline } from "./Timeline";

const baseMatchMedia = () => ({
  matches: true,
  media: "(max-width: 640px)",
  onchange: null,
  addEventListener: vi.fn(),
  removeEventListener: vi.fn(),
  addListener: vi.fn(),
  removeListener: vi.fn(),
  dispatchEvent: vi.fn(),
});

const mockEvents = [
  {
    title: "Jump Sequence Initiated",
    description: "The crew charts their first jump between folded currents.",
    date: "2078-01-01",
  },
  {
    title: "Contact at Meridian Gate",
    description: "An envoy from the Tide Wardens shares a map fragment.",
    date: "2080-05-22",
  },
];

describe("Timeline", () => {
  beforeEach(() => {
    window.matchMedia = vi.fn().mockImplementation(baseMatchMedia) as unknown as typeof window.matchMedia;
  });

  it("renders the provided events", () => {
    render(<Timeline events={mockEvents} />);

    for (const event of mockEvents) {
      expect(screen.getByText(event.title)).toBeInTheDocument();
      expect(screen.getByText(event.description)).toBeInTheDocument();
    }
  });

  it("allows the user to toggle orientation", async () => {
    const user = userEvent.setup();
    render(<Timeline events={mockEvents} />);

    const timelineRegion = screen.getByTestId("timeline-root");
    const horizontalButton = screen.getByRole("button", { name: /horizontal/i });
    const verticalButton = screen.getByRole("button", { name: /vertical/i });

    expect(verticalButton).toHaveAttribute("aria-pressed", "true");
    expect(horizontalButton).toHaveAttribute("aria-pressed", "false");
    expect(timelineRegion).toHaveAttribute("data-orientation", "vertical");

    await user.click(horizontalButton);

    expect(horizontalButton).toHaveAttribute("aria-pressed", "true");
    expect(verticalButton).toHaveAttribute("aria-pressed", "false");
    expect(timelineRegion).toHaveAttribute("data-orientation", "horizontal");
  });
});
