# Next.js Timeline App — High-Level Build Steps

A minimal, single-page timeline viewer for a TV/Movie franchise. Ingests a JSON array of timeline objects; each contains an `events` array where every event has a `title`, `description`, and `date`. Uses existing packages, minimal testing, no auth, no user state, and only a single GitHub link in the UI. Timeline orientation is togglable (vertical/horizontal) and defaults to vertical on mobile.

## 0) Prerequisites
- Node 18+ and npm (or pnpm/yarn)
- GitHub repository URL on hand (for the footer link)

## 1) Scaffold the Project
- Run: `npx create-next-app@latest timeline-franchise --ts --eslint --app --tailwind`
- Choose: App Router, TypeScript, Tailwind (others optional). No src directory is fine.

## 2) Install Core Dependencies
- Timeline UI: `react-chrono`
- Date utilities (optional): `date-fns`
- Commands:
  - `npm i react-chrono date-fns`

## 3) Add Timeline Data (JSON)
- Create `data/timelines.json` (inside the project root) or `app/data/timelines.json`.
- Shape: a JSON array of timeline objects; each object includes an `events` array of entries.
- Example:
```json
[
  {
    "id": "star-wars",
    "title": "Star Wars",
    "backgroundImage": "/images/star-wars.jpg",
    "events": [
      {
        "title": "A New Hope Releases",
        "description": "Episode IV premieres in theaters.",
        "date": "1977-05-25"
      },
      {
        "title": "The Empire Strikes Back",
        "description": "Episode V continues the saga.",
        "date": "1980-05-21"
      }
    ]
  }
]
```
- Notes:
  - `date` should be ISO-8601 (YYYY-MM-DD). Include time if needed.
  - If only one franchise is needed, keep the array with a single object.

## 4) Place Background Assets
- Add franchise images under `public/images/` (e.g., `/images/star-wars.jpg`).
- Use subtle, high-resolution backgrounds; ensure text legibility with an overlay.

## 5) Create the Timeline Component (Client)
- Add `components/Timeline.tsx` with `'use client'` and `react-chrono`.
- Props: `events` (array of `{ title, description, date }`), `initialOrientation`.
- Map JSON events to `react-chrono` items (e.g., `title`, `cardTitle`, `cardSubtitle`, `cardDetailedText`).
- Add a simple toggle (buttons or a segmented control) for `vertical` ⇄ `horizontal`.

## 6) Build the Page (Server)
- In `app/page.tsx` (Server Component):
  - Import the JSON (e.g., `import timelines from "../data/timelines.json";`).
  - Select the first (or the desired) timeline object.
  - Render background image as a full-bleed container with a gradient overlay.
  - Pass the selected timeline’s `events` to the client `Timeline` component.
  - Add a footer with a single link to the GitHub repository.

## 7) Orientation Defaults and Toggle Behavior
- Default: vertical on mobile screens, horizontal on larger screens if desired.
- Implementation:
  - In the client component, detect screen size on mount (`window.matchMedia('(max-width: 640px)')`).
  - Set initial orientation to `vertical` on small screens, otherwise `horizontal` (or keep vertical everywhere if you prefer).
  - Provide a visible toggle with clear labels; do not persist state.

## 8) Minimal Styling
- Use Tailwind for layout, typography, and overlay:
  - Full-viewport container with background image via utility classes or a CSS module.
  - Add a dark translucent overlay to boost contrast.
  - Keep typography simple, high-contrast, and readable.

## 9) Minimal Testing
- Goal: validate that core UI renders and toggle changes orientation; optionally validate data shape.
- Option A (quickest UI smoke test):
  - Install: `npm i -D vitest @testing-library/react @testing-library/jest-dom jsdom`
  - Configure a basic `vitest.config.ts` with `jsdom` environment.
  - Add `components/Timeline.test.tsx` that:
    - Renders with a few mock events.
    - Clicks the toggle and asserts orientation class/prop changes.
- Option B (data-only check):
  - Install `zod`: `npm i zod`
  - Create `scripts/validate-data.ts` to parse `timelines.json` with a Zod schema and exit non-zero on failure.
  - Add `"test": "ts-node scripts/validate-data.ts"` or a `npm run validate:data` script.

## 10) Accessibility & Usability Pass
- Ensure text contrast over backgrounds meets WCAG AA.
- Provide clear focus styles for the toggle control.
- Respect reduced motion preferences if adding animations.

## 11) Metadata and GitHub Link Only
- Set basic metadata (`app/layout.tsx`): title, description.
- Footer: include only a single link to the GitHub repository (no other social links).

## 12) Run and Ship
- Local: `npm run dev` → open `http://localhost:3000`
- Production: `npm run build` then `npm start`
- Deploy: Vercel or any Node host (no env vars needed).

---

### Deliverable Summary
- Single Next.js page rendering a franchise timeline from JSON.
- Minimalistic UI, background imagery, overlay for contrast.
- Orientation toggle (vertical/horizontal); vertical by default on mobile.
- Minimal tests (either a UI smoke test or data shape validation).
- No auth, no user state, no extra nav, only a GitHub link.
