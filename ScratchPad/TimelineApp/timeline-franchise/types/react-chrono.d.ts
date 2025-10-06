declare module "react-chrono" {
  import type { ComponentType, ReactNode } from "react";

  export type TimelineMode = "HORIZONTAL" | "VERTICAL" | "VERTICAL_ALTERNATING";

  export type TimelineItem = {
    title?: ReactNode;
    cardTitle?: ReactNode;
    cardSubtitle?: ReactNode;
    cardDetailedText?: ReactNode;
  };

  export type ChronoTheme = {
    primary?: string;
    secondary?: string;
    cardBgColor?: string;
    titleColor?: string;
    cardTitleColor?: string;
    cardSubtitleColor?: string;
    cardDetailsColor?: string;
  };

  export type ChronoProps = {
    items: TimelineItem[];
    mode?: TimelineMode;
    cardHeight?: number;
    borderLessCards?: boolean;
    disableToolbar?: boolean;
    slideShow?: boolean;
    theme?: ChronoTheme;
  };

  export const Chrono: ComponentType<ChronoProps>;
}
