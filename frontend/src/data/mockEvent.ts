import type { EventDetail } from '@/types/event'

export const mockEvent: EventDetail = {
  startDate: '2026-07-10',
  startTime: '16:00:00',
  entryTime: '15:00:00',
  price: 89.9,
  priceType: 'Vorverkauf',
  eventType: 'Festival',
  location: {
    postalCode: '79400',
    street: 'Feldweg',
    houseNumber: '3',
    city: 'Kandern',
    district: 'Altstadt',
    region: 'Südbaden',
    state: 'Baden-Württemberg',
    eventLocationName: 'Festwiese Kandern',
  },
  concert: null,
  party: null,
  festival: {
    endDate: '2026-07-12',
    endTime: '23:30:00',
    organizer: 'Festival GmbH',
    festivalName: 'Wacken Open Air',
    bands: [
      {
        band: {
          name: 'The Kanders Beats',
          musicians: [
            { artistName: 'Max Muster', firstName: 'Max', lastName: 'Mustermann' },
            { artistName: 'Lisa Sample', firstName: 'Lisa', lastName: 'Sampler' },
          ],
          genres: ['Rock', 'Punk'],
          songs: ['Sommernacht', 'Feuer und Flamme'],
        },
        date: '2026-07-10',
        time: '17:00:00',
        day: 'Freitag',
      },
      {
        band: {
          name: 'DJ Sample',
          musicians: [{ artistName: 'DJ Sample', firstName: null, lastName: null }],
          genres: ['Electronic'],
          songs: null,
        },
        date: '2026-07-11',
        time: '22:00:00',
        day: 'Samstag',
      },
    ],
  },
}
