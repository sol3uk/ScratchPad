import timelinesData from '../data/timelines.json';
import Timeline, { TimelineEvent } from '@/components/Timeline';

interface TimelineDefinition {
  id: string;
  title: string;
  backgroundImage: string;
  summary: string;
  events: TimelineEvent[];
}

const timelines = timelinesData as TimelineDefinition[];

export default function Home() {
  const [featuredTimeline, ...additionalTimelines] = timelines;
  const activeTimeline = featuredTimeline ?? {
    id: 'untitled',
    title: 'Untitled Timeline',
    backgroundImage: '/images/stellaris-vista.svg',
    summary: 'No timeline data was provided.',
    events: [],
  };

  return (
    <div className="relative min-h-screen overflow-hidden text-slate-100">
      <div
        className="absolute inset-0"
        style={{
          backgroundImage: `url(${activeTimeline.backgroundImage})`,
          backgroundSize: 'cover',
          backgroundPosition: 'center',
        }}
        aria-hidden="true"
      />
      <div className="absolute inset-0 bg-slate-950/75" aria-hidden="true" />
      <div className="absolute inset-0 bg-gradient-to-b from-transparent via-slate-950/40 to-slate-950" aria-hidden="true" />

      <main className="relative z-10 mx-auto flex min-h-screen w-full max-w-6xl flex-col gap-16 px-6 py-12 sm:px-12">
        <header className="flex flex-col gap-6 sm:gap-8">
          <div className="flex flex-wrap items-center justify-between gap-4">
            <span className="rounded-full bg-white/10 px-4 py-1 text-xs font-semibold uppercase tracking-[0.3em] text-slate-100 shadow-md shadow-cyan-500/30">
              Franchise Timeline
            </span>
            <span className="text-sm text-slate-200/80">
              Updated {new Date().toLocaleDateString(undefined, { month: 'long', year: 'numeric' })}
            </span>
          </div>
          <h1 className="text-balance text-4xl font-semibold leading-tight text-white sm:text-5xl">
            {activeTimeline.title}
          </h1>
          <p className="max-w-3xl text-lg text-slate-100/90 sm:text-xl">
            {activeTimeline.summary}
          </p>
        </header>

        <section aria-labelledby="timeline-heading" className="flex flex-col gap-12 lg:flex-row">
          <div className="min-w-0 flex-1">
            <h2 id="timeline-heading" className="sr-only">
              {activeTimeline.title} timeline
            </h2>
            <Timeline events={activeTimeline.events} initialOrientation="horizontal" />
          </div>
          {additionalTimelines.length > 0 && (
            <aside className="lg:w-80 lg:flex-shrink-0">
              <h3 className="text-sm font-semibold uppercase tracking-wide text-slate-200">
                More timelines to explore
              </h3>
              <ul className="mt-4 space-y-4">
                {additionalTimelines.map((timeline) => (
                  <li key={timeline.id} className="rounded-2xl border border-white/10 bg-slate-900/50 p-4 shadow-lg shadow-slate-950/40">
                    <p className="text-base font-semibold text-white">{timeline.title}</p>
                    <p className="mt-2 text-sm text-slate-200/80">{timeline.summary}</p>
                    <p className="mt-3 text-xs font-medium uppercase tracking-wider text-slate-400">
                      {timeline.events.length} events available
                    </p>
                  </li>
                ))}
              </ul>
            </aside>
          )}
        </section>
      </main>

      <footer className="relative z-10 border-t border-white/10 bg-slate-950/80 py-6">
        <div className="mx-auto flex w-full max-w-6xl justify-between px-6 sm:px-12">
          <p className="text-xs text-slate-300/80">&copy; {new Date().getFullYear()} Timeline Archive</p>
          <a
            href="https://github.com/example/timeline-franchise"
            target="_blank"
            rel="noopener noreferrer"
            className="text-xs font-semibold uppercase tracking-wide text-cyan-300 transition hover:text-cyan-200 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-cyan-300"
          >
            View on GitHub
          </a>
        </div>
      </footer>
    </div>
  );
}
