import type { Metadata } from "next";
import "./globals.css";

export const metadata: Metadata = {
  title: "Chronicle Timeline Viewer",
  description:
    "An immersive timeline for the Aurora Initiative and Chronorift Saga franchises, featuring rich lore and pivotal events.",
  openGraph: {
    title: "Chronicle Timeline Viewer",
    description:
      "Dive into the Aurora Initiative and Chronorift Saga with an interactive timeline built for quick lore catch-ups.",
    type: "website",
  },
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body className="bg-slate-950 text-slate-100 antialiased">
        {children}
      </body>
    </html>
  );
}
