<template>
  <div class="flex min-h-dvh flex-col bg-ink text-bone">
    <div class="h-1.5 shrink-0 bg-flare"></div>

    <header class="shrink-0 px-6 pt-5 pb-8 lg:px-10">
      <p class="font-body text-xs tracking-[0.2em] text-ash uppercase">Kandern · Spielplan</p>
      <h1 class="mt-2 font-display text-[clamp(2rem,5.5vw,4.25rem)] leading-[0.9] uppercase">
        Veranstaltungen
      </h1>
      <EventFilterBar class="mt-6" :events="allEvents" :today="today" />
    </header>

    <main class="flex-1 px-6 pb-14 lg:px-10">
      <RouterLink
        v-if="nextUp"
        :to="eventRoute(nextUp.id)"
        class="group grid gap-8 bg-flare p-6 text-ink transition-colors hover:bg-flare/90 focus-visible:outline-2 focus-visible:outline-offset-4 focus-visible:outline-flare sm:p-8 lg:grid-cols-[1fr_auto] lg:items-end lg:p-10"
        :class="introClass"
        :style="introStyle(nextUp)"
      >
        <div class="min-w-0">
          <p class="font-body text-xs font-semibold tracking-[0.2em] uppercase">
            Als Nächstes · {{ nextUp.eventType }}
          </p>
          <h2
            class="mt-3 font-display text-[clamp(2.75rem,8vw,6.5rem)] leading-[0.85] break-words uppercase"
            :style="{ viewTransitionName: titleTransitionName(nextUp) }"
          >
            {{ titleOf(nextUp) }}
          </h2>
          <dl class="mt-6 grid grid-cols-2 gap-x-8 gap-y-3 font-body text-sm sm:flex sm:flex-wrap">
            <div v-for="fact in heroFacts" :key="fact.label">
              <dt class="text-[0.65rem] font-semibold tracking-[0.2em] text-ink/60 uppercase">
                {{ fact.label }}
              </dt>
              <dd class="font-semibold">{{ fact.value }}</dd>
            </div>
          </dl>
        </div>

        <div class="flex items-end justify-between gap-6 lg:flex-col lg:items-end lg:text-right">
          <div>
            <p class="font-display text-[clamp(4rem,11vw,9rem)] leading-[0.8] tabular-nums">
              {{ countdownOf(nextUp, today).value }}
            </p>
            <p class="mt-3 font-body text-xs font-semibold tracking-[0.2em] uppercase">
              {{ countdownOf(nextUp, today).unit }}
            </p>
          </div>
          <span
            aria-hidden="true"
            class="font-display text-5xl leading-none transition-transform duration-300 group-hover:translate-x-2"
          >
            &rarr;
          </span>
        </div>
      </RouterLink>

      <section v-if="laterGroups.length" class="mt-14">
        <h2 class="flex items-baseline gap-2 font-display text-xl uppercase">
          <span class="text-sm text-flare">01</span> Weitere Termine
        </h2>
        <div v-for="group in laterGroups" :key="group.key" class="mt-4">
          <h3
            class="sticky top-0 z-10 border-b border-bone/20 bg-ink/90 py-2 font-body text-xs font-semibold tracking-[0.2em] text-ash uppercase backdrop-blur-sm"
          >
            {{ group.label }}
          </h3>
          <ul>
            <EventRow
              v-for="event in group.events"
              :key="event.id"
              :event="event"
              :today="today"
              :class="introClass"
              :style="introStyle(event)"
            />
          </ul>
        </div>
      </section>

      <section v-if="pastEvents.length" class="mt-14">
        <h2 class="flex items-baseline gap-2 font-display text-xl uppercase">
          <span class="text-sm text-flare">{{ laterGroups.length ? '02' : '01' }}</span>
          Vergangen
        </h2>
        <ul class="mt-4 border-t border-bone/20">
          <EventRow
            v-for="event in pastEvents"
            :key="event.id"
            :event="event"
            :today="today"
            :class="introClass"
            :style="introStyle(event)"
          />
        </ul>
      </section>

      <div v-if="!filteredEvents.length" class="py-24 text-center">
        <p class="font-display text-3xl uppercase">Keine Termine</p>
        <p class="mt-2 font-body text-sm text-ash">Für diese Auswahl gibt es keine Termine.</p>
        <RouterLink
          :to="filterLink({ type: null, from: null, to: null })"
          replace
          class="mt-6 inline-block font-body text-xs tracking-[0.2em] text-flare uppercase transition-colors hover:text-bone"
        >
          Filter zurücksetzen
        </RouterLink>
      </div>
    </main>

    <footer class="shrink-0 border-t border-dashed border-bone/30 px-6 py-4 lg:px-10">
      <div
        class="flex flex-wrap justify-between gap-x-8 gap-y-2 font-body text-xs tracking-[0.2em] text-ash uppercase"
      >
        <p>
          {{ upcoming.length }}
          {{ upcoming.length === 1 ? 'kommender Termin' : 'kommende Termine' }}
        </p>
        <p>Alle Angaben ohne Gewähr</p>
      </div>
    </footer>
  </div>
</template>

<script lang="ts">
let introPlayed = false
</script>

<script setup lang="ts">
import { computed } from 'vue'
import { RouterLink } from 'vue-router'
import EventFilterBar from '@/components/EventFilterBar.vue'
import EventRow from '@/components/EventRow.vue'
import { matchesFilters, useEventFilters } from '@/composables/useEventFilters'
import { listEvents } from '@/data/mockEvents'
import { eventRoute, titleTransitionName } from '@/router'
import type { EventDetail } from '@/types/event'
import {
  countdownOf,
  fallback,
  isPast,
  lineupOf,
  monthOf,
  plainTime,
  posterDate,
  priceLabelOf,
  startOfToday,
  titleOf,
} from '@/utils/eventFormat'

const today = startOfToday()
const allEvents = listEvents()
const { filters, filterLink } = useEventFilters()

const playIntro = !introPlayed
introPlayed = true

document.title = 'Veranstaltungen · Kanders Events'

const filteredEvents = computed(() =>
  allEvents.filter((event) => matchesFilters(event, filters.value)),
)

const upcoming = computed(() => filteredEvents.value.filter((event) => !isPast(event, today)))
const nextUp = computed(() => upcoming.value[0] ?? null)
const pastEvents = computed(() =>
  filteredEvents.value.filter((event) => isPast(event, today)).reverse(),
)

const laterGroups = computed(() => {
  const groups: { key: string; label: string; events: EventDetail[] }[] = []
  for (const event of upcoming.value.slice(1)) {
    const month = monthOf(event.startDate)
    const current = groups.at(-1)
    if (current?.key === month.key) current.events.push(event)
    else groups.push({ ...month, events: [event] })
  }
  return groups
})

const heroFacts = computed(() => {
  const event = nextUp.value
  if (!event) return []
  return [
    { label: 'Datum', value: posterDate(event) },
    { label: 'Beginn', value: plainTime(event.startTime) },
    { label: 'Ort', value: fallback(event.location?.eventLocationName) },
    { label: 'Eintritt', value: priceLabelOf(event) },
    { label: 'Line-up', value: lineupOf(event) },
  ]
})

const displayOrder = computed(
  () => new Map([...upcoming.value, ...pastEvents.value].map((event, index) => [event.id, index])),
)

const introClass = playIntro ? 'animate-rise motion-reduce:animate-none' : ''

function introStyle(event: EventDetail) {
  if (!playIntro) return undefined
  const index = Math.min(displayOrder.value.get(event.id) ?? 0, 8)
  return { animationDelay: `${index * 70}ms` }
}
</script>
