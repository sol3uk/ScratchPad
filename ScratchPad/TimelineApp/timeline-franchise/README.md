# Timeline Franchise App

A single-page Next.js experience for browsing fictional franchise timelines. Data is sourced from a structured JSON file and
rendered with an interactive orientation toggle powered by [`react-chrono`](https://github.com/prabhuignoto/react-chrono).

## Getting Started

Install dependencies and run the development server:

```bash
npm install
npm run dev
```

Open http://localhost:3000 to view the app. The timeline orientation defaults to vertical on small screens and can be toggled
between vertical and horizontal via the controls above the chronology.

## Data

Timeline content lives in [`data/timelines.json`](./data/timelines.json). Each timeline entry can include:

```json
{
  "id": "stellaris-odyssey",
  "title": "Stellaris Odyssey",
  "backgroundImage": "/images/stellaris-vista.svg",
  "summary": "A sweeping space opera...",
  "events": [
    {
      "title": "Launch of the Horizon",
      "description": "The experimental starship Horizon embarks...",
      "date": "2227-03-15"
    }
  ]
}
```

Add more franchises or events by editing this file and placing matching background imagery under `public/images/`.

## Testing

A Vitest suite verifies that the orientation toggle works and respects mobile defaults:

```bash
npm run test
```

## Building

Create an optimized production build with:

```bash
npm run build
```
