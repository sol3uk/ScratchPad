'use client';

import { Chrono } from 'react-chrono';
import { useEffect, useMemo, useState } from 'react';
import { format } from 'date-fns';

export type TimelineEvent = {
  title: string;
  description: string;
  date: string;
};

export type Orientation = 'horizontal' | 'vertical';

export interface TimelineProps {
  events: TimelineEvent[];
  initialOrientation?: Orientation;
}

function getOrientationFromMediaQuery(match: boolean, fallback: Orientation): Orientation {
  return match ? 'vertical' : fallback;
}

export function Timeline({ events, initialOrientation = 'horizontal' }: TimelineProps) {
  const [orientation, setOrientation] = useState<Orientation>(initialOrientation);

  useEffect(() => {
    if (typeof window === 'undefined') {
      return;
    }

    const mediaQuery = window.matchMedia('(max-width: 640px)');

    const handleChange = (event: MediaQueryList | MediaQueryListEvent) => {
      const matches = 'matches' in event ? event.matches : event.currentTarget?.matches;
      if (typeof matches === 'boolean') {
        setOrientation(getOrientationFromMediaQuery(matches, initialOrientation));
      }
    };

    handleChange(mediaQuery);
    mediaQuery.addEventListener('change', handleChange);

    return () => {
      mediaQuery.removeEventListener('change', handleChange);
    };
  }, [initialOrientation]);

  const items = useMemo(
    () =>
      events.map((event) => ({
        title: event.title,
        cardTitle: event.title,
        cardSubtitle: format(new Date(event.date), 'MMMM d, yyyy'),
        cardDetailedText: event.description,
      })),
    [events],
  );

  return (
    <div className="w-full">
      <div className="flex items-center justify-between gap-4 pb-6">
        <span className="text-sm font-semibold uppercase tracking-wide text-slate-100">Orientation</span>
        <div className="flex rounded-full bg-white/10 p-1 text-sm font-medium text-slate-100 shadow-inner" role="group" aria-label="Timeline orientation toggle">
          {(['vertical', 'horizontal'] as Orientation[]).map((value) => (
            <button
              key={value}
              type="button"
              onClick={() => setOrientation(value)}
              className={`rounded-full px-4 py-1 transition-colors focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-white ${
                orientation === value ? 'bg-white text-slate-900 shadow-md' : 'hover:bg-white/20'
              }`}
              aria-pressed={orientation === value}
              data-testid={`orientation-${value}`}
            >
              {value === 'vertical' ? 'Vertical' : 'Horizontal'}
            </button>
          ))}
        </div>
      </div>
      <div className="rounded-3xl bg-slate-900/30 p-4 backdrop-blur" data-testid="timeline" data-orientation={orientation}>
        <Chrono
          items={items}
          mode={orientation}
          cardWidth={360}
          theme={{
            primary: '#f8fafc',
            secondary: '#e2e8f0',
            cardBgColor: '#0f172a',
            cardForeColor: '#f8fafc',
            titleColor: '#f8fafc',
          }}
          scrollable
          useReadMore
        />
      </div>
    </div>
  );
}

export default Timeline;
