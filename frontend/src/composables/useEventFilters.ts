import { computed } from 'vue'
import { useRoute, useRouter, type LocationQueryValue } from 'vue-router'
import { EVENT_TYPES, type EventDetail, type EventType } from '@/types/event'
import { inDateRange, toDate } from '@/utils/eventFormat'

export interface EventFilters {
  type: EventType | null
  from: string | null
  to: string | null
}

const ISO_DATE = /^\d{4}-\d{2}-\d{2}$/

function dateParam(value: LocationQueryValue | LocationQueryValue[] | undefined): string | null {
  return typeof value === 'string' && ISO_DATE.test(value) && toDate(value) ? value : null
}

export function matchesFilters(event: EventDetail, filters: EventFilters): boolean {
  return (
    (!filters.type || event.eventType === filters.type) &&
    inDateRange(event, filters.from, filters.to)
  )
}

export function useEventFilters() {
  const route = useRoute()
  const router = useRouter()

  const filters = computed<EventFilters>(() => {
    const type = EVENT_TYPES.find((item) => item === route.query.typ) ?? null
    const from = dateParam(route.query.von)
    const to = dateParam(route.query.bis)
    return from && to && from > to ? { type, from: to, to: from } : { type, from, to }
  })

  const isFiltered = computed(() => Object.values(filters.value).some(Boolean))

  function filterLink(patch: Partial<EventFilters>) {
    const next = { ...filters.value, ...patch }
    const query: Record<string, string> = {}
    if (next.type) query.typ = next.type
    if (next.from) query.von = next.from
    if (next.to) query.bis = next.to
    return { query }
  }

  function setFilters(patch: Partial<EventFilters>) {
    return router.replace(filterLink(patch))
  }

  return { filters, isFiltered, filterLink, setFilters }
}
