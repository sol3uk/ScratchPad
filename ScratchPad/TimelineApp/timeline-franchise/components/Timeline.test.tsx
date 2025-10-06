import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";

vi.mock("react-chrono", () => ({
  Chrono: ({ items }: { items: Array<{ cardTitle: string }> }) => (
    <div data-testid="chrono-mock">
      {items.map((item) => (
        <div key={item.cardTitle}>{item.cardTitle}</div>
      ))}
    </div>
  ),
}));

import Timeline, { type TimelineEvent } from "./Timeline";

describe("Timeline", () => {
  const events: TimelineEvent[] = [
    {
      title: "Test Event One",
      description: "Something notable happens in the cosmos.",
      date: "2099-01-01",
    },
    {
      title: "Test Event Two",
      description: "Time agents neutralise a paradox.",
      date: "2100-06-15",
    },
  ];

  beforeEach(() => {
    Object.defineProperty(window, "matchMedia", {
      writable: true,
      value: vi.fn().mockImplementation((query) => ({
        matches: false,
        media: query,
        onchange: null,
        addListener: vi.fn(),
        removeListener: vi.fn(),
        addEventListener: vi.fn(),
        removeEventListener: vi.fn(),
        dispatchEvent: vi.fn(),
      })),
    });
  });

  it("renders the supplied events", () => {
    render(<Timeline events={events} />);

    expect(screen.getByRole("button", { name: /horizontal/i })).toHaveAttribute(
      "aria-pressed",
      "true"
    );
    expect(screen.getByText(/test event one/i)).toBeInTheDocument();
    expect(screen.getByText(/test event two/i)).toBeInTheDocument();
  });

  it("allows switching to vertical orientation", async () => {
    const user = userEvent.setup();
    render(<Timeline events={events} />);

    const verticalButton = screen.getByRole("button", { name: /vertical/i });
    expect(verticalButton).toHaveAttribute("aria-pressed", "false");

    await user.click(verticalButton);

    expect(verticalButton).toHaveAttribute("aria-pressed", "true");
  });
});
