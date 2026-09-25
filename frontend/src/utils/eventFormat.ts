import type { BandSlot, EventDetail, Musician } from '@/types/event'

export const EMPTY = '—'

interface EventVariant {
  organizer: string | null
  bands: BandSlot[] | null
  endDate?: string | null
  endTime?: string | null
}

/** Konzert, Party oder Festival – je nachdem, welche Variante gefüllt ist. */
export function variantOf(event: EventDetail): EventVariant | null {
  return event.festival ?? event.party ?? event.concert ?? null
}

export function organizerOf(event: EventDetail): string | null {
  return variantOf(event)?.organizer ?? null
}

export function bandsOf(event: EventDetail): BandSlot[] {
  return variantOf(event)?.bands ?? []
}

export function endDateOf(event: EventDetail): string | null {
  return variantOf(event)?.endDate ?? null
}

export function endTimeOf(event: EventDetail): string | null {
  return variantOf(event)?.endTime ?? null
}

/** Festival-/Partyname, bei Konzerten der Headliner, sonst die Location. */
export function titleOf(event: EventDetail): string {
  return (
    event.festival?.festivalName ??
    event.party?.partyName ??
    event.concert?.bands?.[0]?.band.name ??
    event.location?.eventLocationName ??
    event.eventType
  )
}

/** „Nordwind · Fräulein Gold +2“ */
export function lineupOf(event: EventDetail, max = 3): string {
  const names = bandsOf(event).map((slot) => slot.band.name)
  if (!names.length) return EMPTY
  const shown = names.slice(0, max).join(' · ')
  return names.length > max ? `${shown} +${names.length - max}` : shown
}

/**
 * Unterzeile für Listen. Bei Konzerten ist der Headliner schon der Titel –
 * dann die Support-Acts („mit …“) bzw. die Besetzung statt eines doppelten Namens.
 */
export function subtitleOf(event: EventDetail): string {
  const title = titleOf(event)
  const bands = bandsOf(event)
  const others = bands.filter((slot) => slot.band.name !== title)
  if (others.length === bands.length) return lineupOf(event)
  if (others.length) return `mit ${others.map((slot) => slot.band.name).join(' · ')}`
  const musicians = bands[0]?.band.musicians ?? []
  return musicians.length ? musicians.map(musicianName).join(', ') : EMPTY
}

export function priceLabelOf(event: EventDetail): string {
  return event.price === null ? fallback(event.priceType) : formatPrice(event.price)
}

export function placeOf(event: EventDetail): string {
  const parts = [event.location?.city, event.location?.region].filter(Boolean)
  return parts.length ? parts.join(' · ') : EMPTY
}

export function streetOf(event: EventDetail): string {
  const parts = [event.location?.street, event.location?.houseNumber].filter(Boolean)
  return parts.length ? parts.join(' ') : EMPTY
}

export function postalCityOf(event: EventDetail): string {
  const parts = [event.location?.postalCode, event.location?.city].filter(Boolean)
  return parts.length ? parts.join(' ') : EMPTY
}

/** Plakat-Datum: „10. Juli 2026“ bzw. „10.–12. Juli 2026“ bei mehrtägigen Events. */
export function posterDate(event: EventDetail): string {
  const start = toDate(event.startDate)
  if (!start) return EMPTY
  const end = toDate(endDateOf(event))
  const month = (date: Date) => date.toLocaleDateString('de-DE', { month: 'long' })

  if (!end || end.getTime() === start.getTime()) {
    return `${start.getDate()}. ${month(start)} ${start.getFullYear()}`
  }
  if (start.getMonth() === end.getMonth() && start.getFullYear() === end.getFullYear()) {
    return `${start.getDate()}.–${end.getDate()}. ${month(start)} ${start.getFullYear()}`
  }
  return `${start.getDate()}. ${month(start)} – ${end.getDate()}. ${month(end)} ${end.getFullYear()}`
}

export function fallback(value: string | null | undefined): string {
  return value?.trim() ? value : EMPTY
}

export function toDate(value: string | null | undefined): Date | null {
  if (!value) return null
  const date = new Date(`${value}T00:00:00`)
  return Number.isNaN(date.getTime()) ? null : date
}

export function longDate(value: string | null | undefined): string {
  const date = toDate(value)
  if (!date) return EMPTY
  return date.toLocaleDateString('de-DE', { day: '2-digit', month: 'short', year: 'numeric' })
}

export function shortDate(value: string | null | undefined): string {
  const date = toDate(value)
  if (!date) return EMPTY
  return date.toLocaleDateString('de-DE', { day: '2-digit', month: '2-digit' })
}

export function weekday(value: string | null | undefined): string {
  const date = toDate(value)
  return date ? date.toLocaleDateString('de-DE', { weekday: 'long' }) : EMPTY
}

export function plainTime(value: string | null | undefined): string {
  if (!value) return EMPTY
  const [hours, minutes] = value.split(':')
  return `${hours}:${minutes}`
}

export function dateWithTime(
  date: string | null | undefined,
  time: string | null | undefined,
): string {
  const parts = [longDate(date), plainTime(time)].filter((part) => part !== EMPTY)
  return parts.length ? parts.join(', ') : EMPTY
}

export function formatPrice(value: number | null | undefined): string {
  if (value == null) return EMPTY
  return value.toLocaleString('de-DE', { style: 'currency', currency: 'EUR' })
}

export function musicianName(musician: Musician): string {
  const realName = [musician.firstName, musician.lastName].filter(Boolean).join(' ')
  return musician.artistName ?? (realName || EMPTY)
}

/** Alle Genres der Acts, ohne Duplikate. */
export function genresOf(event: EventDetail): string[] {
  return [...new Set(bandsOf(event).flatMap((slot) => slot.band.genres ?? []))]
}

export function isMultiDay(event: EventDetail): boolean {
  const end = endDateOf(event)
  return !!end && end !== event.startDate
}

export function weekdayShort(value: string | null | undefined): string {
  const date = toDate(value)
  return date ? date.toLocaleDateString('de-DE', { weekday: 'short' }).replace('.', '') : EMPTY
}

export function dayOfMonth(value: string | null | undefined): string {
  const date = toDate(value)
  return date ? String(date.getDate()).padStart(2, '0') : EMPTY
}

export function monthShort(value: string | null | undefined): string {
  const date = toDate(value)
  return date ? date.toLocaleDateString('de-DE', { month: 'short' }).replace('.', '') : EMPTY
}

/** Gruppierungs-Schlüssel „2026-10“ plus Anzeigename „Oktober 2026“. */
export function monthOf(value: string | null | undefined): { key: string; label: string } {
  const date = toDate(value)
  if (!date) return { key: 'ohne-datum', label: 'Ohne Datum' }
  return {
    key: `${date.getFullYear()}-${String(date.getMonth() + 1).padStart(2, '0')}`,
    label: date.toLocaleDateString('de-DE', { month: 'long', year: 'numeric' }),
  }
}

// ---------- Zeitbezug zu heute ----------

const DAY_MS = 86_400_000
const relativeFormat = new Intl.RelativeTimeFormat('de-DE', { numeric: 'auto' })

export function startOfToday(): Date {
  const now = new Date()
  return new Date(now.getFullYear(), now.getMonth(), now.getDate())
}

/** Ganze Tage bis zum Datum (negativ = vergangen). */
export function daysUntil(value: string | null | undefined, today: Date): number | null {
  const date = toDate(value)
  return date ? Math.round((date.getTime() - today.getTime()) / DAY_MS) : null
}

export function isPast(event: EventDetail, today: Date): boolean {
  const days = daysUntil(endDateOf(event) ?? event.startDate, today)
  return days !== null && days < 0
}

/** Mehrtägiges Event, das schon vor heute begonnen hat und noch nicht vorbei ist. */
export function isRunning(event: EventDetail, today: Date): boolean {
  const start = daysUntil(event.startDate, today)
  return start !== null && start < 0 && !isPast(event, today)
}

/** „heute“, „morgen“, „in 3 Wochen“, „vor 2 Monaten“ … */
export function relativeLabel(event: EventDetail, today: Date): string {
  if (isRunning(event, today)) return 'läuft gerade'
  const days = daysUntil(event.startDate, today)
  if (days === null) return EMPTY
  const size = Math.abs(days)
  if (size < 14) return relativeFormat.format(days, 'day')
  if (size < 60) return relativeFormat.format(Math.round(days / 7), 'week')
  return relativeFormat.format(Math.round(days / 30), 'month')
}

/** Großer Countdown für das nächste Event: Zahl + Einheit. */
export function countdownOf(event: EventDetail, today: Date): { value: string; unit: string } {
  if (isRunning(event, today)) return { value: 'Jetzt', unit: 'läuft gerade' }
  const days = daysUntil(event.startDate, today)
  if (days === null) return { value: EMPTY, unit: '' }
  if (days === 0) return { value: 'Heute', unit: 'geht’s los' }
  return { value: String(days), unit: days === 1 ? 'Tag bis Beginn' : 'Tage bis Beginn' }
}
