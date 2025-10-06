"use client";

import React from "react";
import { useEffect, useMemo, useState } from "react";
import { Chrono, type Mode } from "react-chrono";
import { format, parseISO } from "date-fns";

type OrientationChoice = Extract<Mode, "HORIZONTAL" | "VERTICAL">;

type TimelineEvent = {
  title: string;
  description: string;
  date: string;
};

type TimelineProps = {
  events: TimelineEvent[];
  /**
   * Sets the starting orientation before responsive logic runs.
   * Pass "horizontal" to force a wide-screen default.
   */
  initialOrientation?: "horizontal" | "vertical";
};

const ORIENTATION_OPTIONS: Array<{
  value: OrientationChoice;
  label: string;
  description: string;
}> = [
  {
    value: "VERTICAL",
    label: "Vertical",
    description: "Best for narrow layouts and mobile devices.",
  },
  {
    value: "HORIZONTAL",
    label: "Horizontal",
    description: "Expands events across wider viewports.",
  },
];

export function Timeline({ events, initialOrientation = "vertical" }: TimelineProps) {
  const [mode, setMode] = useState<OrientationChoice>(
    initialOrientation === "horizontal" ? "HORIZONTAL" : "VERTICAL",
  );
  const [userLockedOrientation, setUserLockedOrientation] = useState(false);

  useEffect(() => {
    const mediaQuery = window.matchMedia("(max-width: 640px)");

    const applyResponsiveOrientation = (matches: boolean) => {
      if (userLockedOrientation) {
        return;
      }
      setMode(matches ? "VERTICAL" : "HORIZONTAL");
    };

    applyResponsiveOrientation(mediaQuery.matches);

    const handleChange = (event: MediaQueryListEvent) => {
      applyResponsiveOrientation(event.matches);
    };

    mediaQuery.addEventListener("change", handleChange);

    return () => {
      mediaQuery.removeEventListener("change", handleChange);
    };
  }, [userLockedOrientation]);

  const items = useMemo(
    () =>
      events.map((event) => {
        const parsedDate = parseISO(event.date);
        const formattedDate = format(parsedDate, "MMMM d, yyyy");

        return {
          title: formattedDate,
          cardTitle: event.title,
          cardSubtitle: formattedDate,
          cardDetailedText: event.description,
        };
      }),
    [events],
  );

  const handleToggle = (nextMode: OrientationChoice) => {
    setMode(nextMode);
    setUserLockedOrientation(true);
  };

  return (
    <section
      role="region"
      aria-label="Aurora Chronicles timeline"
      className="space-y-5"
      data-orientation={mode.toLowerCase()}
      data-testid="timeline-root"
    >
      <div className="flex flex-col gap-3 sm:flex-row sm:items-center sm:justify-between">
        <div>
          <p className="text-xs font-semibold uppercase tracking-[0.35em] text-sky-300/80">
            Orientation
          </p>
          <p className="text-sm text-slate-300">
            Choose how the story should unfold across your screen.
          </p>
        </div>
        <div className="flex flex-wrap gap-2" role="group" aria-label="Toggle timeline orientation">
          {ORIENTATION_OPTIONS.map((option) => {
            const isActive = option.value === mode;
            return (
              <button
                key={option.value}
                type="button"
                onClick={() => handleToggle(option.value)}
                aria-pressed={isActive}
                className={`rounded-full border px-4 py-1.5 text-sm font-medium transition focus:outline-none focus-visible:ring-2 focus-visible:ring-sky-400/80 focus-visible:ring-offset-2 focus-visible:ring-offset-slate-900 ${
                  isActive
                    ? "border-sky-300/80 bg-sky-400/20 text-sky-100"
                    : "border-slate-600/70 bg-slate-900/70 text-slate-200 hover:border-slate-400/70 hover:bg-slate-800/80"
                }`}
                title={option.description}
              >
                {option.label}
              </button>
            );
          })}
        </div>
      </div>
      <div className="rounded-2xl border border-slate-700/60 bg-slate-900/60 p-4 shadow-2xl backdrop-blur" data-mode={mode}>
        <Chrono
          items={items}
          mode={mode}
          disableToolbar
          theme={{
            primary: "#38bdf8",
            secondary: "#0f172a",
            cardBgColor: "rgba(15, 23, 42, 0.92)",
            cardForeColor: "#f8fafc",
            titleColor: "#38bdf8",
            titleColorActive: "#0f172a",
          }}
          scrollable
          cardHeight={200}
        />
      </div>
    </section>
  );
}
