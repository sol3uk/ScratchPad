import React from 'react';
import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { afterEach, describe, expect, test, vi } from 'vitest';
import { Timeline } from './Timeline';

vi.mock('react-chrono', () => ({
  Chrono: ({ mode }: { mode: string }) => (
    <div data-testid="chrono-mock" data-mode={mode}>
      Chrono mock
    </div>
  ),
}));

afterEach(() => {
  vi.restoreAllMocks();
});

const events = [
  {
    title: 'Launch',
    description: 'The mission begins.',
    date: '2227-03-15',
  },
  {
    title: 'Encounter',
    description: 'A mysterious signal appears.',
    date: '2227-05-03',
  },
];

describe('Timeline', () => {
  test('renders events and toggles orientation', async () => {
    const user = userEvent.setup();

    render(<Timeline events={events} initialOrientation="horizontal" />);

    const timeline = screen.getByTestId('timeline');
    expect(timeline).toHaveAttribute('data-orientation', 'horizontal');

    await user.click(screen.getByTestId('orientation-vertical'));
    expect(timeline).toHaveAttribute('data-orientation', 'vertical');

    await user.click(screen.getByTestId('orientation-horizontal'));
    expect(timeline).toHaveAttribute('data-orientation', 'horizontal');
  });

  test('defaults to vertical orientation on small screens', () => {
    const originalMatchMedia = window.matchMedia;
    const listeners: Array<(event: MediaQueryListEvent) => void> = [];

    Object.defineProperty(window, 'matchMedia', {
      writable: true,
      value: (query: string) => ({
        matches: true,
        media: query,
        onchange: null,
        addEventListener: (_event: string, listener: (event: MediaQueryListEvent) => void) => {
          listeners.push(listener);
        },
        removeEventListener: vi.fn(),
        addListener: vi.fn(),
        removeListener: vi.fn(),
        dispatchEvent: vi.fn(),
      }),
    });

    render(<Timeline events={events} initialOrientation="horizontal" />);

    const timeline = screen.getByTestId('timeline');
    expect(timeline).toHaveAttribute('data-orientation', 'vertical');

    Object.defineProperty(window, 'matchMedia', {
      writable: true,
      value: originalMatchMedia,
    });
  });
});
