import Image from "next/image";
import Link from "next/link";

import timelines from "../data/timelines.json";
import { Timeline } from "@/components/Timeline";
import type { TimelineEvent } from "@/components/Timeline";

type TimelineDefinition = {
  id: string;
  title: string;
  tagline: string;
  backgroundImage: string;
  accentColor?: string;
  events: TimelineEvent[];
};

const timelineDefinitions = timelines as TimelineDefinition[];

const [primaryTimeline, ...supportingTimelines] = timelineDefinitions;

const GITHUB_URL = "https://github.com/example/timeline-franchise";

export default function Home() {
  const timeline = primaryTimeline;
  const accent = timeline?.accentColor ?? "#38bdf8";

  return (
    <div className="relative isolate min-h-screen overflow-hidden">
      {timeline ? (
        <>
          <Image
            src={timeline.backgroundImage}
            alt={`${timeline.title} backdrop`}
            fill
            className="absolute inset-0 -z-20 object-cover"
            priority
          />
          <div className="absolute inset-0 -z-10 bg-slate-950/80 backdrop-blur-sm" />
        </>
      ) : null}
      <main className="relative z-10 mx-auto flex min-h-screen max-w-6xl flex-col gap-16 px-6 py-16 sm:px-10 lg:px-16">
        <header className="flex flex-col gap-6 text-center sm:text-left">
          <div className="flex items-center justify-center gap-3 sm:justify-start">
            <span className="rounded-full border border-white/20 bg-white/10 px-4 py-1 text-xs font-semibold uppercase tracking-[0.32em] text-slate-100/80">
              Story Archive
            </span>
            <span className="text-xs font-medium uppercase tracking-[0.32em] text-slate-100/60">
              Adaptive Timeline
            </span>
          </div>
          <div className="space-y-4">
            <h1 className="text-4xl font-semibold sm:text-5xl lg:text-6xl">
              {timeline?.title ?? "Franchise Timeline"}
            </h1>
            <p className="mx-auto max-w-2xl text-base text-slate-200/80 sm:mx-0 sm:text-lg">
              {timeline?.tagline ??
                "Traverse a living timeline that captures the pivotal beats of this fictional franchise."}
            </p>
          </div>
          <p className="mx-auto max-w-3xl text-sm text-slate-200/70 sm:mx-0">
            Explore the curated highlights of our flagship saga. Use the orientation toggle to adapt the layout
            for your screen, and dive into the richly detailed events chronicling breakthroughs, conflicts, and
            renewals across the Confluence.
          </p>
        </header>

        {timeline ? (
          <Timeline events={timeline.events} initialOrientation="horizontal" accentColor={accent} />
        ) : (
          <p className="rounded-3xl border border-white/10 bg-white/5 p-6 text-center text-sm text-slate-200/80">
            No timeline data available.
          </p>
        )}

        {supportingTimelines.length > 0 ? (
          <section aria-labelledby="supporting-timelines" className="space-y-6">
            <div className="flex items-center justify-between gap-4">
              <div>
                <h2 id="supporting-timelines" className="text-xl font-semibold text-slate-100">
                  Other stories from the archive
                </h2>
                <p className="text-sm text-slate-200/70">
                  Glimpse the larger universe with additional arcs ready for future spotlight features.
                </p>
              </div>
            </div>
            <div className="grid gap-6 sm:grid-cols-2">
              {supportingTimelines.map((entry) => {
                const primaryEvent = entry.events[0];
                const nextEvent = entry.events[1];
                return (
                  <article
                    key={entry.id}
                    className="group relative overflow-hidden rounded-3xl border border-white/10 bg-slate-900/60 shadow-lg shadow-black/40"
                  >
                    <div
                      className="absolute inset-0 -z-10 bg-cover bg-center opacity-80 transition group-hover:scale-105 group-hover:opacity-90"
                      style={{ backgroundImage: `url(${entry.backgroundImage})` }}
                      aria-hidden="true"
                    />
                    <div className="absolute inset-0 -z-10 bg-gradient-to-br from-slate-950/80 via-slate-950/70 to-slate-950/90" />
                    <div className="flex h-full flex-col gap-4 p-6">
                      <div className="flex items-center gap-3">
                        <span
                          className="h-2 w-2 rounded-full"
                          style={{ backgroundColor: entry.accentColor ?? accent }}
                          aria-hidden="true"
                        />
                        <h3 className="text-lg font-semibold text-slate-100">{entry.title}</h3>
                      </div>
                      <p className="text-sm text-slate-200/80">{entry.tagline}</p>
                      <dl className="space-y-2 text-xs text-slate-200/70">
                        {primaryEvent ? (
                          <div>
                            <dt className="font-semibold uppercase tracking-widest text-slate-200/60">First Highlight</dt>
                            <dd className="text-slate-100/90">{primaryEvent.title}</dd>
                          </div>
                        ) : null}
                        {nextEvent ? (
                          <div>
                            <dt className="font-semibold uppercase tracking-widest text-slate-200/60">Next Turning Point</dt>
                            <dd className="text-slate-100/90">{nextEvent.title}</dd>
                          </div>
                        ) : null}
                      </dl>
                    </div>
                  </article>
                );
              })}
            </div>
          </section>
        ) : null}
      </main>
      <footer className="relative z-10 border-t border-white/10 bg-slate-950/80 py-6">
        <div className="mx-auto flex max-w-6xl items-center justify-center px-6 sm:px-10 lg:px-16">
          <Link
            href={GITHUB_URL}
            className="text-sm font-medium text-slate-200/80 transition hover:text-white focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-white"
          >
            View the project on GitHub
          </Link>
        </div>
      </footer>
    </div>
  );
}
