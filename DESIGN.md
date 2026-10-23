# GolBet Design System

## Visual Theme

Restrained product interface inspired by a clear afternoon scoreboard: precise blue-green structure, crisp white surfaces, deep ink, and a warm amber reserved for odds and highlights. The experience should feel like a sports utility, not a promotional betting page.

## Color Palette

- `--color-bg: oklch(1 0 0)` - page background.
- `--color-surface: oklch(0.975 0.006 210)` - secondary surface.
- `--color-ink: oklch(0.22 0.025 220)` - primary text.
- `--color-muted: oklch(0.47 0.025 220)` - secondary text.
- `--color-primary: oklch(0.50 0.105 210)` - navigation, primary actions, selected controls.
- `--color-primary-dark: oklch(0.36 0.09 210)` - hover and high-emphasis identity.
- `--color-accent: oklch(0.72 0.15 70)` - odds and small highlights.
- Semantic colors use distinct icons/text in addition to hue.

## Typography

Use the system sans-serif stack for a dependable, legible university project. Headings are compact and confident; body copy is 1rem with comfortable line height. Data such as scores and odds uses tabular numerals.

## Components

- Top navigation: solid primary color, compact brand mark, high-contrast links.
- Match list: responsive two-column grid collapsing to one column; cards use a defined shadow without an additional decorative border.
- Filters: compact pill controls with a clear selected state and keyboard focus.
- Search: labeled GET form with persistent value, adjacent action, and responsive stacking.
- Status badges: icon/word pair so meaning is not color-only.
- Empty state: plain explanation plus a single route back to the complete fixture list.
- Forms and tables: consistent controls, visible validation, horizontal overflow on narrow screens.

## Layout

Use a centered content width of approximately 1120px. Keep page spacing rhythmic and preserve a 44px minimum interactive target where practical. Product typography remains fixed-size; responsive changes are structural.

## Motion

Transitions last 150-220ms and communicate hover, focus, or state changes only. Respect `prefers-reduced-motion` and do not stage page-load animations.
