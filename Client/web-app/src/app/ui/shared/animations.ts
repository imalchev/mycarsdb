import { animate, AnimationEntryMetadata, state, style, transition, trigger } from '@angular/core';

// Component transition animations
export const slideInDownAnimation: AnimationEntryMetadata =
  trigger('transition', [
    state('in',
      style({
        opacity: 1,
        transform: 'translateY(0)'
      })
    ),
    transition('void=>*', [
      style({
        opacity: 0,
        transform: 'translateY(-155px)'
      }),
      animate(1200)
    ])
  ]);

