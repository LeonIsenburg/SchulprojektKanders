import type { EventDetail } from '@/types/event'

export const mockEvents: EventDetail[] = [
  {
    id: 1,
    startDate: '2026-10-09',
    startTime: '20:00:00',
    entryTime: '19:00:00',
    price: 24.5,
    priceType: 'Abendkasse',
    eventType: 'Konzert',
    location: {
      postalCode: '79400',
      street: 'Hauptstraße',
      houseNumber: '12',
      city: 'Kandern',
      district: 'Zentrum',
      region: 'Südbaden',
      state: 'Baden-Württemberg',
      eventLocationName: 'Stadthalle Kandern',
    },
    concert: {
      organizer: 'Kulturkreis Kandern',
      bands: [
        {
          band: {
            name: 'Nordwind',
            musicians: [
              { artistName: null, firstName: 'Jonas', lastName: 'Berger' },
              { artistName: null, firstName: 'Mira', lastName: 'Keller' },
              { artistName: 'Tomcat', firstName: 'Thomas', lastName: 'Ritter' },
            ],
            genres: ['Indie', 'Folk'],
            songs: ['Über den Dächern', 'Kalter Kaffee', 'Bleib noch'],
          },
          date: '2026-10-09',
          time: '20:30:00',
          day: 'Freitag',
        },
        {
          band: {
            name: 'Fräulein Gold',
            musicians: [{ artistName: 'Fräulein Gold', firstName: 'Nele', lastName: 'Gold' }],
            genres: ['Singer-Songwriter'],
            songs: ['Goldstaub'],
          },
          date: '2026-10-09',
          time: '22:15:00',
          day: 'Freitag',
        },
      ],
    },
    party: null,
    festival: null,
  },
  {
    id: 2,
    startDate: '2026-10-31',
    startTime: '21:00:00',
    entryTime: '20:30:00',
    price: 12,
    priceType: 'Vorverkauf',
    eventType: 'Party',
    location: {
      postalCode: '79400',
      street: 'Weberstraße',
      houseNumber: '8',
      city: 'Kandern',
      district: 'Gewerbegebiet',
      region: 'Südbaden',
      state: 'Baden-Württemberg',
      eventLocationName: 'Alte Weberei',
    },
    concert: null,
    party: {
      endDate: '2026-11-01',
      endTime: '04:00:00',
      organizer: 'Nachtschicht Events',
      partyName: 'Neonnacht',
      bands: [
        {
          band: {
            name: 'DJ Halcyon',
            musicians: [{ artistName: 'DJ Halcyon', firstName: 'Sarah', lastName: 'Lind' }],
            genres: ['House'],
            songs: null,
          },
          date: '2026-10-31',
          time: '21:30:00',
          day: 'Samstag',
        },
        {
          band: {
            name: 'Kollektiv Rausch',
            musicians: [
              { artistName: 'Rausch', firstName: 'Ben', lastName: 'Wagner' },
              { artistName: 'Nova', firstName: 'Ida', lastName: 'Frank' },
            ],
            genres: ['Techno', 'Electro'],
            songs: null,
          },
          date: '2026-11-01',
          time: '01:00:00',
          day: 'Sonntag',
        },
      ],
    },
    festival: null,
  },
  {
    id: 3,
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
  },
  {
    id: 4,
    startDate: '2027-06-18',
    startTime: '17:00:00',
    entryTime: '16:00:00',
    price: 59,
    priceType: 'Tageskasse',
    eventType: 'Festival',
    location: {
      postalCode: '79400',
      street: 'Am Waldstadion',
      houseNumber: '1',
      city: 'Kandern',
      district: 'Holzen',
      region: 'Südbaden',
      state: 'Baden-Württemberg',
      eventLocationName: 'Waldstadion Kandern',
    },
    concert: null,
    party: null,
    festival: {
      endDate: '2027-06-19',
      endTime: '23:30:00',
      organizer: 'Kandertal Kultur e. V.',
      festivalName: 'Kandertal Open Air',
      bands: [
        {
          band: {
            name: 'Betonherz',
            musicians: [
              { artistName: null, firstName: 'Lukas', lastName: 'Brandt' },
              { artistName: null, firstName: 'Ayse', lastName: 'Demir' },
            ],
            genres: ['Alternative', 'Post-Punk'],
            songs: ['Grauzone', 'Nachtschicht'],
          },
          date: '2027-06-18',
          time: '18:00:00',
          day: 'Freitag',
        },
        {
          band: {
            name: 'Silberpappel',
            musicians: [{ artistName: null, firstName: 'Hanna', lastName: 'Voss' }],
            genres: ['Dream Pop'],
            songs: ['Flimmern'],
          },
          date: '2027-06-18',
          time: '20:30:00',
          day: 'Freitag',
        },
        {
          band: {
            name: 'Südhang',
            musicians: [
              { artistName: 'Schorsch', firstName: 'Georg', lastName: 'Maier' },
              { artistName: null, firstName: 'Paul', lastName: 'Renz' },
            ],
            genres: ['Reggae', 'Ska'],
            songs: ['Rebstock', 'Sonnenseite'],
          },
          date: '2027-06-19',
          time: '19:00:00',
          day: 'Samstag',
        },
      ],
    },
  },
  {
    id: 5,
    startDate: '2026-12-12',
    startTime: '19:30:00',
    entryTime: '19:00:00',
    price: null,
    priceType: 'Eintritt frei',
    eventType: 'Konzert',
    location: {
      postalCode: '79400',
      street: 'Kirchplatz',
      houseNumber: '1',
      city: 'Kandern',
      district: 'Zentrum',
      region: 'Südbaden',
      state: 'Baden-Württemberg',
      eventLocationName: 'Stadtkirche Kandern',
    },
    concert: {
      organizer: 'Musikverein Kandern',
      bands: [
        {
          band: {
            name: 'Kammerchor Kandern',
            musicians: [
              { artistName: null, firstName: 'Ruth', lastName: 'Sommer' },
              { artistName: null, firstName: 'Martin', lastName: 'Eberle' },
            ],
            genres: ['Klassik', 'Chor'],
            songs: ['Abendlied', 'Requiem (Auszüge)'],
          },
          date: '2026-12-12',
          time: '19:30:00',
          day: 'Samstag',
        },
      ],
    },
    party: null,
    festival: null,
  },
  {
    id: 6,
    startDate: '2026-11-14',
    startTime: '20:00:00',
    entryTime: '19:30:00',
    price: 15,
    priceType: 'Abendkasse',
    eventType: 'Konzert',
    location: {
      postalCode: '79400',
      street: 'Blumenplatz',
      houseNumber: '4',
      city: 'Kandern',
      district: 'Altstadt',
      region: 'Südbaden',
      state: 'Baden-Württemberg',
      eventLocationName: 'Kellerbühne Kandern',
    },
    concert: {
      organizer: 'Jazzclub Kandern e. V.',
      bands: [
        {
          band: {
            name: 'Trio Kanderblau',
            musicians: [
              { artistName: null, firstName: 'Felix', lastName: 'Hausmann' },
              { artistName: null, firstName: 'Clara', lastName: 'Imhof' },
              { artistName: 'Stix', firstName: 'Daniel', lastName: 'Stark' },
            ],
            genres: ['Jazz'],
            songs: ['Blaue Stunde', 'Kandertal Blues', 'Night Train'],
          },
          date: '2026-11-14',
          time: '20:15:00',
          day: 'Samstag',
        },
      ],
    },
    party: null,
    festival: null,
  },
  {
    id: 7,
    startDate: '2027-02-06',
    startTime: '19:00:00',
    entryTime: '18:30:00',
    price: 9,
    priceType: 'Vorverkauf',
    eventType: 'Party',
    location: {
      postalCode: '79400',
      street: 'Hauptstraße',
      houseNumber: '12',
      city: 'Kandern',
      district: 'Zentrum',
      region: 'Südbaden',
      state: 'Baden-Württemberg',
      eventLocationName: 'Stadthalle Kandern',
    },
    concert: null,
    party: {
      endDate: '2027-02-07',
      endTime: '02:00:00',
      organizer: 'Narrenzunft Kandern',
      partyName: 'Zunftball',
      bands: [
        {
          band: {
            name: 'Kanderschränzer',
            musicians: [
              { artistName: null, firstName: 'Tobias', lastName: 'Kiefer' },
              { artistName: null, firstName: 'Julia', lastName: 'Senn' },
            ],
            genres: ['Guggenmusik'],
            songs: ['Narrenmarsch', 'Schränzer-Polka'],
          },
          date: '2027-02-06',
          time: '20:00:00',
          day: 'Samstag',
        },
        {
          band: {
            name: 'DJ Fasnetgeist',
            musicians: [{ artistName: 'DJ Fasnetgeist', firstName: 'Kai', lastName: 'Hug' }],
            genres: ['Party', 'Schlager'],
            songs: null,
          },
          date: '2027-02-06',
          time: '22:30:00',
          day: 'Samstag',
        },
      ],
    },
    festival: null,
  },
]

/** Alle Events chronologisch – so, wie später auch die API sie liefern sollte. */
export function listEvents(): EventDetail[] {
  return [...mockEvents].sort((a, b) => (a.startDate ?? '').localeCompare(b.startDate ?? ''))
}

export function findEvent(id: string | number): EventDetail | null {
  return mockEvents.find((event) => String(event.id) === String(id)) ?? null
}
