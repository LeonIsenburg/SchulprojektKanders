export type EventType = 'Konzert' | 'Party' | 'Festival'

export interface Musician {
  artistName: string | null
  firstName: string | null
  lastName: string | null
}

export interface Band {
  name: string
  musicians: Musician[] | null
  genres: string[] | null
  songs: string[] | null
}

export interface BandSlot {
  band: Band
  date: string | null
  time: string | null
  day: string | null
}

export interface EventLocation {
  postalCode: string | null
  street: string | null
  houseNumber: string | null
  city: string | null
  district: string | null
  region: string | null
  state: string | null
  eventLocationName: string | null
}

export interface Concert {
  organizer: string | null
  bands: BandSlot[] | null
}

export interface Party {
  endDate: string | null
  endTime: string | null
  organizer: string | null
  partyName: string | null
  bands: BandSlot[] | null
}

export interface Festival {
  endDate: string | null
  endTime: string | null
  organizer: string | null
  festivalName: string | null
  bands: BandSlot[] | null
}

export interface EventDetail {
  startDate: string | null
  startTime: string | null
  entryTime: string | null
  price: number | null
  priceType: string | null
  eventType: EventType
  location: EventLocation | null
  concert: Concert | null
  party: Party | null
  festival: Festival | null
}
