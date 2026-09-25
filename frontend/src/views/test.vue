<template>
  <div class="app-layout flex min-h-dvh flex-col bg-ink text-bone lg:h-dvh lg:overflow-hidden">
    <div class="h-1.5 shrink-0 bg-flare"></div>

    <header class="shrink-0 px-6 pt-5 pb-4 lg:px-10">
      <div class="flex flex-wrap items-end justify-between gap-x-8 gap-y-2">
        <div>
          <p class="font-body text-xs tracking-[0.2em] text-ash uppercase">
            {{ event.eventType }}
            <span v-if="event.location?.eventLocationName">
              &mdash; {{ event.location.eventLocationName }}</span
            >
          </p>
          <h1 class="mt-2 font-display text-[clamp(2rem,5.5vw,4.25rem)] leading-[0.9] uppercase">
            {{ title }}
          </h1>
        </div>
        <div class="pb-1 text-left sm:text-right">
          <p class="font-display text-[clamp(1.25rem,2.5vw,2rem)] text-flare uppercase">
            {{ posterDate }}
          </p>
          <p class="font-body text-sm text-ash">
            {{ [event.location?.city, event.location?.region].filter(Boolean).join(' · ') }}
          </p>
        </div>
      </div>
    </header>

    <main class="flex min-h-0 flex-1 items-center px-6 pb-6 lg:px-10">
      <div class="grid w-full grid-cols-1 gap-x-12 gap-y-10 lg:grid-cols-[1.6fr_1fr_1fr]">
      <section v-if="bands.length" class="min-w-0">
        <h2 class="flex items-baseline gap-2 font-display text-xl uppercase">
          <span class="text-sm text-flare">01</span> Line-up
        </h2>
        <table class="mt-3 w-full table-fixed border-t border-bone/20 text-left">
          <tbody>
            <tr v-for="slot in bands" :key="slot.band.name" class="border-b border-bone/10 align-top">
              <td class="w-24 py-5 pr-3">
                <div class="font-display text-base text-flare uppercase">
                  {{ (slot.day ?? weekday(slot.date)).slice(0, 2) }}
                </div>
                <div class="font-body text-xs text-ash">{{ shortDate(slot.date) }}</div>
                <div class="font-display mt-1 text-2xl">{{ plainTime(slot.time) }}</div>
              </td>
              <td class="py-5">
                <div class="font-display text-[clamp(1.5rem,2.8vw,2.5rem)] leading-tight break-words uppercase">
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
                    <dd class="text-bone/75">{{ slot.band.musicians.map(musicianName).join(', ') }}</dd>
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
              <th scope="row" class="w-2/5 py-3 pr-3 font-normal tracking-wider text-ash uppercase">
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
      <dl class="flex flex-wrap gap-x-10 gap-y-3 font-display text-lg uppercase">
        <div>
          <dt class="font-body text-[0.65rem] tracking-[0.2em] text-ash">Einlass</dt>
          <dd>{{ plainTime(event.entryTime) }}</dd>
        </div>
        <div>
          <dt class="font-body text-[0.65rem] tracking-[0.2em] text-ash">Eintritt</dt>
          <dd class="text-flare">{{ formatPrice(event.price) }}</dd>
        </div>
        <div>
          <dt class="font-body text-[0.65rem] tracking-[0.2em] text-ash">Preistyp</dt>
          <dd>{{ fallback(event.priceType) }}</dd>
        </div>
        <div>
          <dt class="font-body text-[0.65rem] tracking-[0.2em] text-ash">Veranstalter</dt>
          <dd>{{ fallback(details?.organizer) }}</dd>
        </div>
      </dl>
    </footer>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { mockEvent } from '@/data/mockEvent'
import type { Musician } from '@/types/event'

const EMPTY = '—'

const event = mockEvent

const details = computed(() => event.festival ?? event.party ?? event.concert ?? null)

const endDate = computed(() => event.festival?.endDate ?? event.party?.endDate ?? null)

const endTime = computed(() => event.festival?.endTime ?? event.party?.endTime ?? null)

const eventName = computed(() => {
  if (event.festival) return event.festival.festivalName
  if (event.party) return event.party.partyName
  return null
})

const title = computed(
  () => eventName.value ?? event.location?.eventLocationName ?? event.eventType,
)

const posterDate = computed(() => {
  const start = toDate(event.startDate)
  if (!start) return EMPTY
  const end = toDate(endDate.value)
  const month = (date: Date) => date.toLocaleDateString('de-DE', { month: 'long' })

  if (!end || end.getTime() === start.getTime()) {
    return `${start.getDate()}. ${month(start)} ${start.getFullYear()}`
  }
  if (start.getMonth() === end.getMonth() && start.getFullYear() === end.getFullYear()) {
    return `${start.getDate()}.–${end.getDate()}. ${month(start)} ${start.getFullYear()}`
  }
  return `${start.getDate()}. ${month(start)} – ${end.getDate()}. ${month(end)} ${end.getFullYear()}`
})

const bands = computed(() => details.value?.bands ?? [])

const sections = computed(() => [
  {
    title: 'Termin',
    rows: [
      { label: 'Art', value: event.eventType },
      { label: 'Beginn', value: dateWithTime(event.startDate, event.startTime) },
      { label: 'Einlass', value: plainTime(event.entryTime) },
      { label: 'Ende', value: dateWithTime(endDate.value, endTime.value) },
    ],
  },
  {
    title: 'Ort',
    rows: [
      { label: 'Location', value: fallback(event.location?.eventLocationName) },
      { label: 'Adresse', value: street() },
      { label: 'PLZ / Ort', value: postalCity() },
      { label: 'Stadtteil', value: fallback(event.location?.district) },
      { label: 'Region', value: fallback(event.location?.region) },
      { label: 'Bundesland', value: fallback(event.location?.state) },
    ],
  },
])

function sectionNumber(index: number): string {
  return String(index + (bands.value.length ? 2 : 1)).padStart(2, '0')
}

function fallback(value: string | null | undefined): string {
  return value?.trim() ? value : EMPTY
}

function street(): string {
  const parts = [event.location?.street, event.location?.houseNumber].filter(Boolean)
  return parts.length ? parts.join(' ') : EMPTY
}

function postalCity(): string {
  const parts = [event.location?.postalCode, event.location?.city].filter(Boolean)
  return parts.length ? parts.join(' ') : EMPTY
}

function toDate(value: string | null | undefined): Date | null {
  if (!value) return null
  const date = new Date(`${value}T00:00:00`)
  return Number.isNaN(date.getTime()) ? null : date
}

function longDate(value: string | null | undefined): string {
  const date = toDate(value)
  if (!date) return EMPTY
  return date.toLocaleDateString('de-DE', { day: '2-digit', month: 'short', year: 'numeric' })
}

function shortDate(value: string | null | undefined): string {
  const date = toDate(value)
  if (!date) return EMPTY
  return date.toLocaleDateString('de-DE', { day: '2-digit', month: '2-digit' })
}

function weekday(value: string | null | undefined): string {
  const date = toDate(value)
  return date ? date.toLocaleDateString('de-DE', { weekday: 'long' }) : EMPTY
}

function plainTime(value: string | null | undefined): string {
  if (!value) return EMPTY
  const [hours, minutes] = value.split(':')
  return `${hours}:${minutes}`
}

function dateWithTime(date: string | null | undefined, time: string | null | undefined): string {
  const parts = [longDate(date), plainTime(time)].filter((part) => part !== EMPTY)
  return parts.length ? parts.join(', ') : EMPTY
}

function formatPrice(value: number | null | undefined): string {
  if (value == null) return EMPTY
  return value.toLocaleString('de-DE', { style: 'currency', currency: 'EUR' })
}

function musicianName(musician: Musician): string {
  const realName = [musician.firstName, musician.lastName].filter(Boolean).join(' ')
  return musician.artistName ?? (realName || EMPTY)
}
</script>
