import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import "./globals.css";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "Franchise Timeline Explorer",
  description:
    "Immerse yourself in the defining story beats of the Aetherion explorers, the Eclipse Fleet, and the guardians of Wyrmwood.",
  openGraph: {
    title: "Franchise Timeline Explorer",
    description:
      "Follow three richly crafted universes with an adaptive timeline experience powered by React Chrono.",
  },
  twitter: {
    card: "summary_large_image",
    title: "Franchise Timeline Explorer",
    description:
      "Follow three richly crafted universes with an adaptive timeline experience powered by React Chrono.",
  },
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en" className="bg-slate-950">
      <body
        className={`${geistSans.variable} ${geistMono.variable} bg-slate-950 text-slate-100 antialiased`}
      >
        {children}
      </body>
    </html>
  );
}
