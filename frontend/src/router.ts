import { nextTick } from 'vue'
import { createRouter, createWebHashHistory, START_LOCATION } from 'vue-router'

const routes = [
  { path: '/', name: 'events', component: () => import('./views/EventListView.vue') },
  {
    path: '/events/:id',
    name: 'event-detail',
    component: () => import('./views/EventDetailView.vue'),
    props: true,
  },
  { path: '/:pathMatch(.*)*', redirect: '/' },
]

const router = createRouter({
  history: createWebHashHistory(),
  routes,
  scrollBehavior(to, from, savedPosition) {
    // Zurück-Navigation: dort weitermachen, wo man war
    if (savedPosition) return savedPosition
    // Nur Filter (Query) geändert: Scroll-Position behalten
    if (to.path === from.path) return false
    return { top: 0 }
  },
})

// Seitenwechsel über die View Transitions API animieren. Elemente mit gleichem
// `view-transition-name` (z. B. der Event-Titel) morphen dabei von Liste zu Detail.
let finishViewTransition: (() => void) | null = null

router.beforeResolve((to, from) => {
  const reducedMotion = window.matchMedia('(prefers-reduced-motion: reduce)').matches
  if (from === START_LOCATION || reducedMotion || !('startViewTransition' in document)) return

  finishViewTransition?.()
  return new Promise<void>((proceed) => {
    document.startViewTransition(
      () =>
        new Promise<void>((done) => {
          finishViewTransition = done
          proceed()
        }),
    )
  })
})

router.afterEach(async () => {
  // DOM-Update und Scroll-Wiederherstellung abwarten, erst dann den neuen Zustand freigeben
  await nextTick()
  await new Promise((resolve) => setTimeout(resolve))
  finishViewTransition?.()
  finishViewTransition = null
})

/** Route zur Detailansicht eines Events. */
export function eventRoute(id: number) {
  return { name: 'event-detail', params: { id } }
}

/** Gemeinsamer Name, damit der Titel beim Seitenwechsel von Liste zu Detail morpht. */
export function titleTransitionName(event: { id: number }): string {
  return `event-title-${event.id}`
}

export default router
