import Image from "next/image";
import Link from "next/link";
import { format, parseISO } from "date-fns";

import timelines from "../data/timelines.json";
import { Timeline } from "@/components/Timeline";

const [timeline] = timelines;

const eventRange = timeline
  ? (() => {
      const sortedDates = [...timeline.events]
        .map((event) => parseISO(event.date))
        .sort((a, b) => a.getTime() - b.getTime());
      if (sortedDates.length === 0) {
        return null;
      }
      const first = format(sortedDates[0], "MMMM d, yyyy");
      const last = format(sortedDates[sortedDates.length - 1], "MMMM d, yyyy");
      return { first, last, count: sortedDates.length };
    })()
  : null;

export default function Home() {
  if (!timeline) {
    return (
      <main className="flex min-h-screen items-center justify-center bg-slate-950 text-slate-200">
        <p>No timeline data found.</p>
      </main>
    );
  }

  return (
    <div className="relative min-h-screen overflow-hidden">
      <div className="absolute inset-0 -z-20">
        <Image
          src={timeline.backgroundImage}
          alt={`${timeline.title} background`}
          fill
          priority
          className="object-cover"
          sizes="100vw"
        />
      </div>
      <div className="absolute inset-0 -z-10 bg-gradient-to-b from-slate-950/85 via-slate-950/70 to-slate-950/95" />

      <div className="relative mx-auto flex min-h-screen w-full max-w-5xl flex-col px-6 py-12 sm:px-10 lg:px-14">
        <header className="mb-10 sm:mb-14">
          <p className="text-xs font-semibold uppercase tracking-[0.35em] text-sky-300/80">
            {timeline.id.replace(/-/g, " ").toUpperCase()}
          </p>
          <h1 className="mt-3 text-4xl font-semibold text-white sm:text-5xl">{timeline.title}</h1>
          {timeline.tagline ? (
            <p className="mt-4 max-w-2xl text-lg text-slate-200 sm:text-xl">
              {timeline.tagline}
            </p>
          ) : null}
          {eventRange ? (
            <p className="mt-4 text-sm uppercase tracking-[0.3em] text-slate-300">
              {eventRange.count} mission briefs • {eventRange.first} → {eventRange.last}
            </p>
          ) : null}
        </header>

        <main className="flex-1 pb-10">
          <Timeline events={timeline.events} />
        </main>

        <footer className="mt-8 border-t border-white/10 pt-6 text-sm text-slate-300">
          <Link
            href="https://github.com/example/timeline-franchise"
            className="transition hover:text-sky-200"
          >
            View the project on GitHub
          </Link>
        </footer>
      </div>
    </div>
  );
}
