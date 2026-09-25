<template>
  <li class="border-b border-bone/10">
    <RouterLink
      :to="eventRoute(event.id)"
      class="group relative grid grid-cols-[3.5rem_1fr] gap-x-5 gap-y-3 py-5 pr-2 pl-4 transition hover:bg-bone/5 focus-visible:bg-bone/5 focus-visible:outline-2 focus-visible:-outline-offset-2 focus-visible:outline-flare sm:grid-cols-[4.5rem_1fr_auto] sm:items-center sm:gap-x-8 sm:pl-5"
      :class="{ 'opacity-55 hover:opacity-100 focus-visible:opacity-100': past }"
    >
      <span
        aria-hidden="true"
        class="absolute inset-y-0 left-0 w-1 origin-bottom scale-y-0 bg-flare transition-transform duration-300 group-hover:scale-y-100 group-focus-visible:scale-y-100"
      ></span>

      <div class="text-center">
        <p class="font-body text-[0.65rem] tracking-[0.2em] text-ash uppercase">
          {{ weekdayShort(event.startDate) }}
        </p>
        <p class="font-display text-4xl leading-none sm:text-5xl">
          {{ dayOfMonth(event.startDate) }}
        </p>
        <p class="mt-1 font-body text-xs font-semibold tracking-[0.2em] text-flare uppercase">
          {{ monthShort(event.startDate) }}
        </p>
        <p v-if="isMultiDay(event)" class="mt-0.5 font-body text-[0.65rem] text-ash">
          bis {{ shortDate(endDateOf(event)) }}
        </p>
      </div>

      <div class="min-w-0">
        <p class="truncate font-body text-xs tracking-[0.2em] text-ash uppercase">
          {{ event.eventType }}
          <template v-if="event.location?.eventLocationName">
            &mdash; {{ event.location.eventLocationName }}
          </template>
          · {{ plainTime(event.startTime) }}
        </p>
        <h3
          class="mt-1 font-display text-[clamp(1.5rem,3vw,2.25rem)] leading-tight break-words uppercase transition-colors group-hover:text-flare group-focus-visible:text-flare"
          :style="{ viewTransitionName: titleTransitionName(event) }"
        >
          {{ titleOf(event) }}
        </h3>
        <p v-if="subtitle !== EMPTY" class="mt-1 font-body text-sm text-bone/75">
          {{ subtitle }}
        </p>
        <p
          v-if="genres.length"
          class="mt-1 font-body text-xs tracking-[0.15em] text-flare/80 uppercase"
        >
          {{ genres.join(' · ') }}
        </p>
      </div>

      <div
        class="col-start-2 flex items-center justify-between gap-4 sm:col-start-auto sm:flex-col sm:items-end sm:gap-1.5 sm:text-right"
      >
        <p
          class="font-body text-xs font-semibold tracking-[0.2em] uppercase"
          :class="past ? 'text-ash' : 'text-flare'"
        >
          {{ relativeLabel(event, today) }}
        </p>
        <p class="font-display text-xl leading-none sm:text-2xl">{{ priceLabelOf(event) }}</p>
        <span
          aria-hidden="true"
          class="hidden font-display text-2xl leading-none text-ash transition group-hover:translate-x-1 group-hover:text-flare sm:block"
        >
          &rarr;
        </span>
      </div>
    </RouterLink>
  </li>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { RouterLink } from 'vue-router'
import { eventRoute, titleTransitionName } from '@/router'
import type { EventDetail } from '@/types/event'
import {
  dayOfMonth,
  EMPTY,
  endDateOf,
  genresOf,
  isMultiDay,
  isPast,
  monthShort,
  plainTime,
  priceLabelOf,
  relativeLabel,
  shortDate,
  subtitleOf,
  titleOf,
  weekdayShort,
} from '@/utils/eventFormat'

const props = defineProps<{ event: EventDetail; today: Date }>()

const past = computed(() => isPast(props.event, props.today))
const subtitle = computed(() => subtitleOf(props.event))
const genres = computed(() => genresOf(props.event).slice(0, 3))
</script>
