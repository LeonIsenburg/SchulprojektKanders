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
    if (savedPosition) return savedPosition
    if (to.path === from.path) return false
    return { top: 0 }
  },
})

let finishViewTransition: (() => void) | null = null

router.beforeResolve((to, from) => {
  const reducedMotion = window.matchMedia('(prefers-reduced-motion: reduce)').matches
  if (from === START_LOCATION || reducedMotion || !('startViewTransition' in document)) return

  finishViewTransition?.()
  return new Promise<void>((proceed) => {
    const transition = document.startViewTransition(
      () =>
        new Promise<void>((done) => {
          finishViewTransition = done
          proceed()
        }),
    )
    transition.ready.catch(() => {})
  })
})

router.afterEach(async () => {
  await nextTick()
  await new Promise((resolve) => setTimeout(resolve))
  finishViewTransition?.()
  finishViewTransition = null
})

export function eventRoute(id: number) {
  return { name: 'event-detail', params: { id } }
}

export function titleTransitionName(event: { id: number }): string {
  return `event-title-${event.id}`
}

export default router
