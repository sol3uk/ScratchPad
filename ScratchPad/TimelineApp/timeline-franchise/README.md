# Chronicle Timeline Viewer

An immersive single-page timeline viewer for sci-fi TV and film franchises. The app is built with Next.js (App Router), Tailwind CSS, and `react-chrono`, loading structured data directly from `data/timelines.json`.

## Features

- **Franchise timelines** sourced from JSON with titles, descriptions, and event metadata.
- **Orientation toggle** to swap between horizontal and vertical modes (vertical is enforced on small screens).
- **Rich background art** per franchise with contrast-preserving overlays.
- **Accessibility-focused controls** with keyboard focus styles and descriptive copy.
- **Automated tests** powered by Vitest and Testing Library to exercise the orientation toggle.

## Getting Started

Install dependencies and run the development server:

```bash
npm install
npm run dev
```

Then open [http://localhost:3000](http://localhost:3000) to explore the timeline.

## Available Scripts

- `npm run dev` – start the dev server with Turbopack.
- `npm run build` – generate an optimized production build.
- `npm run start` – serve the production build.
- `npm run lint` – run ESLint with the Next.js configuration.
- `npm run test` – execute Vitest unit tests.

## Project Structure

```
app/
  layout.tsx      # Metadata and global frame
  page.tsx        # Renders the active franchise timeline
components/
  Timeline.tsx    # Client timeline wrapper around react-chrono
  Timeline.test.tsx
public/images/    # Franchise background illustrations
```

## Data Source

Timeline content lives in [`data/timelines.json`](data/timelines.json). Each entry provides:

- `id`: unique identifier used for referencing assets.
- `title` and `description`: display content.
- `backgroundImage`: path to the background artwork.
- `primaryColor`: accent color for timeline elements.
- `events`: ordered array of `{ title, description, date }` items rendered by the timeline.

Update or extend this file to showcase additional franchises or story arcs.

## Testing

Vitest is configured with Testing Library and a mocked `react-chrono` component to validate that the timeline renders events and that the orientation toggle works as expected.

Run the suite with:

```bash
npm run test
```

## Deployment

The project is deployment-ready for platforms like Vercel. Build the optimized output with `npm run build` and serve it via `npm run start`.
