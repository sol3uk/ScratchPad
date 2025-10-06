import Image from "next/image";
import Timeline, { type TimelineEvent } from "@/components/Timeline";
import timelineData from "../data/timelines.json";

type FranchiseTimeline = {
  id: string;
  title: string;
  description: string;
  backgroundImage: string;
  primaryColor?: string;
  events: TimelineEvent[];
};

const timelines = timelineData as FranchiseTimeline[];

export default function Home() {
  const [activeTimeline, ...otherTimelines] = timelines;

  if (!activeTimeline) {
    return (
      <main className="flex min-h-screen items-center justify-center bg-slate-950 text-slate-200">
        <p>No timeline data was found.</p>
      </main>
    );
  }

  return (
    <main className="relative min-h-screen overflow-hidden text-slate-100">
      <div className="absolute inset-0 -z-10">
        <Image
          src={activeTimeline.backgroundImage}
          alt={`${activeTimeline.title} background art`}
          fill
          priority
          className="object-cover"
        />
        <div className="absolute inset-0 bg-slate-950/75 backdrop-blur-sm" />
      </div>

      <div className="relative mx-auto flex min-h-screen w-full max-w-5xl flex-col gap-12 px-6 py-16 sm:px-10 lg:px-12">
        <header className="space-y-4">
          <p className="text-xs uppercase tracking-[0.5em] text-sky-200/70">Franchise Timeline</p>
          <h1 className="text-4xl font-semibold tracking-tight text-slate-50 sm:text-5xl">
            {activeTimeline.title}
          </h1>
          <p className="max-w-2xl text-base leading-relaxed text-slate-200/85 sm:text-lg">
            {activeTimeline.description}
          </p>
        </header>

        <Timeline
          events={activeTimeline.events}
          accentColor={activeTimeline.primaryColor}
          initialOrientation="horizontal"
        />

        {otherTimelines.length > 0 && (
          <section className="space-y-3 rounded-2xl border border-white/10 bg-slate-900/40 p-6 backdrop-blur">
            <h2 className="text-lg font-semibold text-slate-100">Also in the Archive</h2>
            <ul className="grid gap-4 sm:grid-cols-2">
              {otherTimelines.map((timeline) => (
                <li key={timeline.id} className="rounded-xl border border-white/10 bg-slate-900/40 p-4">
                  <p className="text-sm uppercase tracking-wider text-slate-400">{timeline.id.replace(/-/g, " ")}</p>
                  <p className="text-base font-medium text-slate-100">{timeline.title}</p>
                  <p className="mt-1 text-sm text-slate-300/80">
                    {timeline.events.length} curated events
                  </p>
                </li>
              ))}
            </ul>
          </section>
        )}

        <footer className="mt-auto flex flex-col gap-2 border-t border-white/10 pt-8 text-sm text-slate-300/80 sm:flex-row sm:items-center sm:justify-between">
          <span>Chronicle compiled by the Interstellar Archives.</span>
          <a
            className="font-medium text-sky-200 transition hover:text-sky-100 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-sky-200"
            href="https://github.com/example/timeline-franchise"
            target="_blank"
            rel="noreferrer"
          >
            View the project on GitHub
          </a>
        </footer>
      </div>
    </main>
  );
}
