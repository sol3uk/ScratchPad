'use client';

import { Chrono, TimelineMode } from 'react-chrono';
import { useEffect, useMemo, useState } from 'react';
import { format, parseISO } from 'date-fns';

type TimelineEvent = {
  title: string;
  description: string;
  date: string;
};

type Orientation = 'vertical' | 'horizontal';

type TimelineProps = {
  events: TimelineEvent[];
  initialOrientation?: Orientation;
  accentColor?: string;
};

const ORIENTATION_TO_MODE: Record<Orientation, TimelineMode> = {
  vertical: 'VERTICAL',
  horizontal: 'HORIZONTAL',
};

const baseButtonClass =
  'rounded-full px-4 py-1 text-sm font-medium transition focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-white';

const formatEventDate = (value: string) => {
  try {
    const parsed = parseISO(value);
    return {
      short: format(parsed, 'MMM d, yyyy'),
      long: format(parsed, "EEEE, MMMM d, yyyy"),
    };
  } catch (error) {
    return {
      short: value,
      long: value,
    };
  }
};

export function Timeline({
  events,
  initialOrientation = 'horizontal',
  accentColor = '#38bdf8',
}: TimelineProps) {
  const [orientation, setOrientation] = useState<Orientation>(initialOrientation);

  useEffect(() => {
    if (typeof window === 'undefined' || typeof window.matchMedia !== 'function') {
      return;
    }

    const media = window.matchMedia('(max-width: 640px)');

    const applyOrientation = (matches: boolean) => {
      setOrientation(matches ? 'vertical' : initialOrientation);
    };

    applyOrientation(media.matches);

    const listener = (event: MediaQueryListEvent) => {
      applyOrientation(event.matches);
    };

    media.addEventListener('change', listener);

    return () => {
      media.removeEventListener('change', listener);
    };
  }, [initialOrientation]);

  const items = useMemo(
    () =>
      events.map((event) => {
        const date = formatEventDate(event.date);
        return {
          title: date.short,
          cardTitle: event.title,
          cardSubtitle: date.long,
          cardDetailedText: event.description,
        };
      }),
    [events],
  );

  const mode = ORIENTATION_TO_MODE[orientation];

  const activeButtonClass = `${baseButtonClass} bg-white text-slate-900 shadow-lg shadow-slate-900/40`;
  const inactiveButtonClass = `${baseButtonClass} text-slate-100/90 hover:bg-white/10`;

  return (
    <div
      className="rounded-3xl border border-white/10 bg-slate-950/60 p-6 shadow-xl shadow-black/40 backdrop-blur"
      data-orientation={orientation}
      data-testid="timeline-container"
    >
      <div className="mb-6 flex flex-wrap items-center justify-between gap-4">
        <h2 className="text-lg font-semibold text-slate-100">Timeline Orientation</h2>
        <div className="inline-flex items-center gap-2 rounded-full border border-white/20 bg-white/5 p-1">
          <button
            type="button"
            onClick={() => setOrientation('vertical')}
            className={orientation === 'vertical' ? activeButtonClass : inactiveButtonClass}
            aria-pressed={orientation === 'vertical'}
          >
            Vertical
          </button>
          <button
            type="button"
            onClick={() => setOrientation('horizontal')}
            className={orientation === 'horizontal' ? activeButtonClass : inactiveButtonClass}
            aria-pressed={orientation === 'horizontal'}
          >
            Horizontal
          </button>
        </div>
      </div>
      <Chrono
        items={items}
        mode={mode}
        allowDynamicUpdate
        theme={{
          primary: accentColor,
          secondary: '#1f2937',
          cardBgColor: '#0f172a',
          cardTitleColor: '#e2e8f0',
          cardSubtitleColor: '#94a3b8',
          titleColor: '#cbd5f5',
          titleColorActive: accentColor,
        }}
        useReadMore={false}
        cardHeight={orientation === 'horizontal' ? 220 : undefined}
      />
    </div>
  );
}

export type { TimelineEvent, Orientation };
