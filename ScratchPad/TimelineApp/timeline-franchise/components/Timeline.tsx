"use client";

import { Chrono, type TimelineMode } from "react-chrono";
import { useEffect, useMemo, useState } from "react";
import { format, parseISO } from "date-fns";
import clsx from "clsx";

export type TimelineEvent = {
  title: string;
  description: string;
  date: string;
};

type Orientation = "horizontal" | "vertical";

type TimelineProps = {
  events: TimelineEvent[];
  initialOrientation?: Orientation;
  accentColor?: string;
};

const toChronoMode = (orientation: Orientation): TimelineMode => {
  return orientation === "horizontal" ? "HORIZONTAL" : "VERTICAL";
};

const ORIENTATION_LABEL: Record<Orientation, string> = {
  horizontal: "Horizontal",
  vertical: "Vertical",
};

export function Timeline({
  events,
  initialOrientation = "horizontal",
  accentColor = "#38bdf8",
}: TimelineProps) {
  const [orientation, setOrientation] = useState<Orientation>(initialOrientation);

  useEffect(() => {
    if (typeof window === "undefined" || typeof window.matchMedia !== "function") {
      return;
    }

    const mediaQuery = window.matchMedia("(max-width: 640px)");

    if (mediaQuery.matches) {
      setOrientation("vertical");
    }
  }, []);

  const items = useMemo(
    () =>
      events.map((event) => {
        const parsedDate = parseISO(event.date);
        const formatted = Number.isNaN(parsedDate.getTime())
          ? event.date
          : format(parsedDate, "MMM d, yyyy");

        return {
          title: formatted,
          cardTitle: event.title,
          cardSubtitle: formatted,
          cardDetailedText: event.description,
        };
      }),
    [events]
  );

  return (
    <div className="w-full space-y-6">
      <div className="flex items-center justify-end gap-2" role="group" aria-label="Timeline orientation toggle">
        {(Object.keys(ORIENTATION_LABEL) as Orientation[]).map((value) => (
          <button
            key={value}
            type="button"
            onClick={() => setOrientation(value)}
            className={clsx(
              "rounded-full border px-4 py-1.5 text-sm font-medium transition focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2",
              value === orientation
                ? "bg-white/90 text-slate-900 shadow border-transparent focus-visible:outline-white"
                : "bg-slate-800/60 text-slate-200 border-slate-500/50 hover:bg-slate-700/70 focus-visible:outline-white"
            )}
            aria-pressed={orientation === value}
          >
            {ORIENTATION_LABEL[value]}
          </button>
        ))}
      </div>
      <div className="rounded-2xl border border-white/10 bg-slate-900/40 backdrop-blur-sm p-4 shadow-xl">
        <Chrono
          items={items}
          mode={toChronoMode(orientation)}
          theme={{
            primary: accentColor,
            secondary: "#0f172a",
            cardBgColor: "#020617cc",
            titleColor: "#e2e8f0",
            cardTitleColor: "#f8fafc",
            cardSubtitleColor: "#bae6fd",
            cardDetailsColor: "#cbd5f5",
          }}
          cardHeight={200}
          borderLessCards
          disableToolbar
          slideShow
        />
      </div>
    </div>
  );
}

export default Timeline;
