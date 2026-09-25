<template>
  <div v-if="event" class="flex min-h-dvh flex-col bg-ink text-bone lg:h-dvh lg:overflow-hidden">
    <div class="h-1.5 shrink-0 bg-flare"></div>

    <header class="shrink-0 px-6 pt-4 pb-4 lg:px-10">
      <div
        class="flex items-center justify-between gap-4 font-body text-xs tracking-[0.2em] text-ash uppercase"
      >
        <RouterLink v-slot="{ href }" :to="{ name: 'events' }" custom>
          <a
            :href="href"
            class="group inline-flex items-center gap-2 transition-colors hover:text-flare focus-visible:text-flare"
            @click.exact.prevent="goBack"
          >
            <span aria-hidden="true" class="transition-transform group-hover:-translate-x-1">
              &larr;
            </span>
            Alle Veranstaltungen
          </a>
        </RouterLink>

        <div class="flex items-center gap-4 tabular-nums">
          <p class="hidden items-center gap-1.5 normal-case tracking-normal lg:flex">
            <kbd class="border border-bone/20 px-1.5 font-body">&larr;</kbd>
            <kbd class="border border-bone/20 px-1.5 font-body">&rarr;</kbd>
            blättern
            <kbd class="ml-2 border border-bone/20 px-1.5 font-body">Esc</kbd>
            zurück
          </p>
          <p>{{ positionLabel }}</p>
        </div>
      </div>

      <div class="mt-5 flex flex-wrap items-end justify-between gap-x-8 gap-y-2">
        <div class="min-w-0">
          <p
            class="flex flex-wrap items-center gap-x-3 gap-y-1 font-body text-xs tracking-[0.2em] text-ash uppercase"
          >
            <span>
              {{ event.eventType }}
              <span v-if="event.location?.eventLocationName">
                &mdash; {{ event.location.eventLocationName }}</span
              >
            </span>
            <span
              class="px-2 py-0.5 font-semibold"
              :class="past ? 'border border-bone/25 text-ash' : 'bg-flare text-ink'"
            >
              {{ past ? 'Vergangen' : relativeLabel(event, today) }}
            </span>
          </p>
          <h1
            class="mt-2 font-display text-[clamp(2rem,5.5vw,4.25rem)] leading-[0.9] break-words uppercase"
            :style="{ viewTransitionName: titleTransitionName(event) }"
          >
            {{ titleOf(event) }}
          </h1>
        </div>
        <div class="pb-1 text-left sm:text-right">
          <p class="font-display text-[clamp(1.25rem,2.5vw,2rem)] text-flare uppercase">
            {{ posterDate(event) }}
          </p>
          <p class="font-body text-sm text-ash">{{ placeOf(event) }}</p>
        </div>
      </div>
    </header>

    <main class="flex min-h-0 flex-1 items-center-safe px-6 pb-6 lg:overflow-y-auto lg:px-10">
      <div class="grid w-full grid-cols-1 gap-x-12 gap-y-10 lg:grid-cols-[1.6fr_1fr_1fr]">
        <section v-if="bands.length" class="min-w-0">
          <h2 class="flex items-baseline gap-2 font-display text-xl uppercase">
            <span class="text-sm text-flare">01</span> Line-up
          </h2>
          <table class="mt-3 w-full table-fixed border-t border-bone/20 text-left">
            <tbody>
              <tr
                v-for="slot in bands"
                :key="slot.band.name"
                class="border-b border-bone/10 align-top"
              >
                <td class="w-24 py-5 pr-3">
                  <div class="font-display text-base text-flare uppercase">
                    {{ (slot.day ?? weekday(slot.date)).slice(0, 2) }}
                  </div>
                  <div class="font-body text-xs text-ash">{{ shortDate(slot.date) }}</div>
                  <div class="mt-1 font-display text-2xl">{{ plainTime(slot.time) }}</div>
                </td>
                <td class="py-5">
                  <div
                    class="font-display text-[clamp(1.5rem,2.8vw,2.5rem)] leading-tight break-words uppercase"
                  >
                    {{ slot.band.name }}
                  </div>
                  <div
                    v-if="slot.band.genres?.length"
                    class="font-body text-sm tracking-[0.15em] text-flare uppercase"
                  >
                    {{ slot.band.genres.join(' · ') }}
                  </div>
                  <dl class="mt-3 space-y-1 font-body text-sm text-ash">
                    <div v-if="slot.band.musicians?.length" class="flex gap-3">
                      <dt class="w-20 shrink-0 uppercase">Besetzung</dt>
                      <dd class="text-bone/75">
                        {{ slot.band.musicians.map(musicianName).join(', ') }}
                      </dd>
                    </div>
                    <div v-if="slot.band.songs?.length" class="flex gap-3">
                      <dt class="w-20 shrink-0 uppercase">Songs</dt>
                      <dd class="text-bone/75">{{ slot.band.songs.join(', ') }}</dd>
                    </div>
                  </dl>
                </td>
              </tr>
            </tbody>
          </table>
        </section>

        <section v-for="(section, index) in sections" :key="section.title" class="min-w-0">
          <h2 class="flex items-baseline gap-2 font-display text-xl uppercase">
            <span class="text-sm text-flare">{{ sectionNumber(index) }}</span> {{ section.title }}
          </h2>
          <table class="mt-3 w-full table-fixed border-t border-bone/20 text-left text-sm">
            <tbody>
              <tr v-for="row in section.rows" :key="row.label" class="border-b border-bone/10">
                <th
                  scope="row"
                  class="w-2/5 py-3 pr-3 font-normal tracking-wider text-ash uppercase"
                >
                  {{ row.label }}
                </th>
                <td class="py-3 text-right font-body text-base break-words">{{ row.value }}</td>
              </tr>
            </tbody>
          </table>
        </section>
      </div>
    </main>

    <footer class="shrink-0 border-t border-dashed border-bone/30 px-6 py-4 lg:px-10">
      <div class="flex flex-wrap items-end justify-between gap-x-10 gap-y-5">
        <dl class="flex flex-wrap gap-x-10 gap-y-3 font-display text-lg uppercase">
          <div>
            <dt class="font-body text-[0.65rem] tracking-[0.2em] text-ash">Einlass</dt>
            <dd>{{ plainTime(event.entryTime) }}</dd>
          </div>
          <div>
            <dt class="font-body text-[0.65rem] tracking-[0.2em] text-ash">Eintritt</dt>
            <dd class="text-flare">{{ priceLabelOf(event) }}</dd>
          </div>
          <div>
            <dt class="font-body text-[0.65rem] tracking-[0.2em] text-ash">Preistyp</dt>
            <dd>{{ fallback(event.priceType) }}</dd>
          </div>
          <div>
            <dt class="font-body text-[0.65rem] tracking-[0.2em] text-ash">Veranstalter</dt>
            <dd>{{ fallback(organizerOf(event)) }}</dd>
          </div>
        </dl>

        <nav aria-label="Weitere Veranstaltungen" class="flex w-full gap-2 sm:w-auto">
          <RouterLink
            v-if="prev"
            :to="eventRoute(prev.id)"
            replace
            class="group min-w-0 flex-1 border border-bone/20 px-4 py-2 transition-colors hover:border-flare focus-visible:border-flare focus-visible:outline-none sm:w-56 sm:flex-none"
          >
            <span class="block font-body text-[0.65rem] tracking-[0.2em] text-ash uppercase">
              &larr; Vorheriges
            </span>
            <span
              class="block truncate font-display text-base uppercase transition-colors group-hover:text-flare"
            >
              {{ titleOf(prev) }}
            </span>
          </RouterLink>
          <RouterLink
            v-if="next"
            :to="eventRoute(next.id)"
            replace
            class="group ml-auto min-w-0 flex-1 border border-bone/20 px-4 py-2 text-right transition-colors hover:border-flare focus-visible:border-flare focus-visible:outline-none sm:w-56 sm:flex-none"
          >
            <span class="block font-body text-[0.65rem] tracking-[0.2em] text-ash uppercase">
              Nächstes &rarr;
            </span>
            <span
              class="block truncate font-display text-base uppercase transition-colors group-hover:text-flare"
            >
              {{ titleOf(next) }}
            </span>
          </RouterLink>
        </nav>
      </div>
    </footer>
  </div>

  <div v-else class="flex min-h-dvh flex-col bg-ink text-bone">
    <div class="h-1.5 shrink-0 bg-flare"></div>
    <main class="flex flex-1 flex-col items-start justify-center gap-4 px-6 lg:px-10">
      <p class="font-body text-xs tracking-[0.2em] text-ash uppercase">Fehler 404</p>
      <h1 class="font-display text-[clamp(2rem,5.5vw,4.25rem)] leading-[0.9] uppercase">
        Veranstaltung nicht gefunden
      </h1>
      <RouterLink
        :to="{ name: 'events' }"
        class="font-body text-sm tracking-[0.2em] text-flare uppercase transition-colors hover:text-bone"
      >
        &larr; Zur Übersicht
      </RouterLink>
    </main>
  </div>
</template>

<script setup lang="ts">
import { computed, onBeforeUnmount, onMounted, watchEffect } from 'vue'
import { RouterLink, useRouter } from 'vue-router'
import { findEvent, listEvents } from '@/data/mockEvents'
import { eventRoute, titleTransitionName } from '@/router'
import {
  bandsOf,
  dateWithTime,
  endDateOf,
  endTimeOf,
  fallback,
  isPast,
  musicianName,
  organizerOf,
  placeOf,
  plainTime,
  postalCityOf,
  posterDate,
  priceLabelOf,
  relativeLabel,
  shortDate,
  startOfToday,
  streetOf,
  titleOf,
  weekday,
} from '@/utils/eventFormat'

const props = defineProps<{ id: string }>()

const router = useRouter()
const today = startOfToday()
const orderedEvents = listEvents()

const event = computed(() => findEvent(props.id))

const position = computed(() => orderedEvents.findIndex((item) => item.id === event.value?.id))
const prev = computed(() =>
  position.value > 0 ? (orderedEvents[position.value - 1] ?? null) : null,
)
const next = computed(() =>
  position.value >= 0 ? (orderedEvents[position.value + 1] ?? null) : null,
)
const positionLabel = computed(
  () => `${twoDigits(position.value + 1)} / ${twoDigits(orderedEvents.length)}`,
)

const past = computed(() => (event.value ? isPast(event.value, today) : false))

const bands = computed(() => (event.value ? bandsOf(event.value) : []))

const sections = computed(() => {
  const current = event.value
  if (!current) return []

  return [
    {
      title: 'Termin',
      rows: [
        { label: 'Art', value: current.eventType },
        { label: 'Beginn', value: dateWithTime(current.startDate, current.startTime) },
        { label: 'Einlass', value: plainTime(current.entryTime) },
        { label: 'Ende', value: dateWithTime(endDateOf(current), endTimeOf(current)) },
      ],
    },
    {
      title: 'Ort',
      rows: [
        { label: 'Location', value: fallback(current.location?.eventLocationName) },
        { label: 'Adresse', value: streetOf(current) },
        { label: 'PLZ / Ort', value: postalCityOf(current) },
        { label: 'Stadtteil', value: fallback(current.location?.district) },
        { label: 'Region', value: fallback(current.location?.region) },
        { label: 'Bundesland', value: fallback(current.location?.state) },
      ],
    },
  ]
})

watchEffect(() => {
  document.title = `${event.value ? titleOf(event.value) : 'Nicht gefunden'} · Kanders Events`
})

function sectionNumber(index: number): string {
  return twoDigits(index + (bands.value.length ? 2 : 1))
}

function twoDigits(value: number): string {
  return String(value).padStart(2, '0')
}

// Kommt man aus der Liste, geht's per History zurück – Filter und Scroll-Position bleiben erhalten.
// Vor/Zurück zwischen Events nutzt `replace`, damit dieser Weg immer direkt zur Liste führt.
function goBack() {
  if (router.options.history.state.back) router.back()
  else router.push({ name: 'events' })
}

function onKeydown(keyEvent: KeyboardEvent) {
  if (keyEvent.defaultPrevented || keyEvent.repeat) return
  if (keyEvent.altKey || keyEvent.ctrlKey || keyEvent.metaKey || keyEvent.shiftKey) return

  if (keyEvent.key === 'Escape') goBack()
  else if (keyEvent.key === 'ArrowLeft' && prev.value) router.replace(eventRoute(prev.value.id))
  else if (keyEvent.key === 'ArrowRight' && next.value) router.replace(eventRoute(next.value.id))
}

onMounted(() => window.addEventListener('keydown', onKeydown))
onBeforeUnmount(() => window.removeEventListener('keydown', onKeydown))
</script>
