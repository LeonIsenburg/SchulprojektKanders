<template>
  <div>
    <div
      class="grid grid-cols-1 gap-x-8 gap-y-3 border-y border-bone/20 py-4 sm:grid-cols-[5.5rem_1fr] sm:items-center"
    >
      <p id="filter-type" class="font-body text-xs tracking-[0.2em] text-ash uppercase">Art</p>
      <div role="group" aria-labelledby="filter-type" class="flex flex-wrap gap-2">
        <FilterChip
          v-for="option in typeOptions"
          :key="option.label"
          :to="filterLink({ type: option.type })"
          :active="filters.type === option.type"
          :count="option.count"
        >
          {{ option.label }}
        </FilterChip>
      </div>

      <p id="filter-range" class="font-body text-xs tracking-[0.2em] text-ash uppercase">
        Zeitraum
      </p>
      <div
        role="group"
        aria-labelledby="filter-range"
        class="flex flex-wrap items-center gap-x-6 gap-y-3"
      >
        <div class="flex flex-wrap gap-2">
          <FilterChip
            v-for="option in presetOptions"
            :key="option.label"
            :to="filterLink({ from: option.from, to: option.to })"
            :active="option.active"
            :count="option.count"
          >
            {{ option.label }}
          </FilterChip>
        </div>

        <div class="flex flex-wrap items-center gap-x-3 gap-y-2">
          <label class="flex items-center gap-2">
            <span class="font-body text-xs tracking-[0.2em] text-ash uppercase">Von</span>
            <input
              type="date"
              :value="filters.from ?? ''"
              :min="MIN_DATE"
              :max="filters.to ?? MAX_DATE"
              class="border bg-transparent px-2 py-1 font-body text-sm text-bone tabular-nums scheme-dark transition-colors hover:border-bone/60 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-flare"
              :class="filters.from ? 'border-flare' : 'border-bone/20'"
              @change="onDateChange('from', $event)"
            />
          </label>
          <span aria-hidden="true" class="text-ash">&ndash;</span>
          <label class="flex items-center gap-2">
            <span class="font-body text-xs tracking-[0.2em] text-ash uppercase">Bis</span>
            <input
              type="date"
              :value="filters.to ?? ''"
              :min="filters.from ?? MIN_DATE"
              :max="MAX_DATE"
              class="border bg-transparent px-2 py-1 font-body text-sm text-bone tabular-nums scheme-dark transition-colors hover:border-bone/60 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-flare"
              :class="filters.to ? 'border-flare' : 'border-bone/20'"
              @change="onDateChange('to', $event)"
            />
          </label>
        </div>
      </div>
    </div>

    <div
      class="mt-3 flex flex-wrap items-baseline justify-between gap-x-6 gap-y-1 font-body text-xs tracking-[0.2em] text-ash uppercase"
    >
      <p aria-live="polite">
        <span class="font-semibold text-bone tabular-nums">{{ resultCount }}</span>
        {{ resultCount === 1 ? 'Termin' : 'Termine' }} · {{ rangeLabel(filters.from, filters.to) }}
        <template v-if="filters.type"> · {{ TYPE_LABELS[filters.type] }}</template>
      </p>
      <RouterLink
        v-if="isFiltered"
        :to="filterLink({ type: null, from: null, to: null })"
        replace
        class="text-flare transition-colors hover:text-bone focus-visible:text-bone"
      >
        Filter zurücksetzen
      </RouterLink>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { RouterLink } from 'vue-router'
import FilterChip from '@/components/FilterChip.vue'
import { matchesFilters, useEventFilters } from '@/composables/useEventFilters'
import { EVENT_TYPES, type EventDetail, type EventType } from '@/types/event'
import { datePresets, inDateRange, rangeLabel } from '@/utils/eventFormat'

const TYPE_LABELS: Record<EventType, string> = {
  Konzert: 'Konzerte',
  Party: 'Partys',
  Festival: 'Festivals',
}

const MIN_DATE = '2000-01-01'
const MAX_DATE = '2100-12-31'

const props = defineProps<{ events: EventDetail[]; today: Date }>()

const { filters, isFiltered, filterLink, setFilters } = useEventFilters()

const presets = datePresets(props.today)

const typeOptions = computed(() => {
  const inRange = props.events.filter((event) =>
    inDateRange(event, filters.value.from, filters.value.to),
  )
  return [
    { label: 'Alle', type: null, count: inRange.length },
    ...EVENT_TYPES.map((type) => ({
      label: TYPE_LABELS[type],
      type,
      count: inRange.filter((event) => event.eventType === type).length,
    })),
  ]
})

const presetOptions = computed(() => {
  const ofType = props.events.filter(
    (event) => !filters.value.type || event.eventType === filters.value.type,
  )
  return [{ label: 'Jederzeit', from: null, to: null }, ...presets].map((option) => ({
    ...option,
    count: ofType.filter((event) => inDateRange(event, option.from, option.to)).length,
    active: filters.value.from === option.from && filters.value.to === option.to,
  }))
})

const resultCount = computed(
  () => props.events.filter((event) => matchesFilters(event, filters.value)).length,
)

function onDateChange(field: 'from' | 'to', event: Event) {
  const value = (event.target as HTMLInputElement).value || null
  if (value && (value < MIN_DATE || value > MAX_DATE)) return

  const { from, to } = filters.value
  if (field === 'from') setFilters({ from: value, to: value && to && value > to ? value : to })
  else setFilters({ to: value, from: value && from && value < from ? value : from })
}
</script>
