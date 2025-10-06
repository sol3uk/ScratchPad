import '@testing-library/jest-dom/vitest';
import { vi } from 'vitest';

declare global {
  interface Window {
    matchMedia: (query: string) => MediaQueryList;
  }
}

Object.defineProperty(window, 'matchMedia', {
  writable: true,
  value: (query: string) => {
    return {
      matches: false,
      media: query,
      onchange: null,
      addEventListener: vi.fn(),
      removeEventListener: vi.fn(),
      addListener: vi.fn(),
      removeListener: vi.fn(),
      dispatchEvent: vi.fn(),
    } as MediaQueryList;
  },
});
