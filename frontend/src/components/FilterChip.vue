<template>
  <RouterLink
    :to="to"
    replace
    :aria-current="active ? 'true' : undefined"
    class="inline-flex items-center gap-2 border px-3 py-1.5 font-body text-xs font-semibold tracking-[0.2em] whitespace-nowrap uppercase transition-colors focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-flare"
    :class="[
      active
        ? 'border-flare bg-flare text-ink'
        : 'border-bone/20 text-bone/75 hover:border-bone/60 hover:text-bone',
      { 'opacity-50 hover:opacity-100': count === 0 && !active },
    ]"
  >
    <slot />
    <span
      v-if="count !== undefined"
      class="tabular-nums"
      :class="active ? 'text-ink/60' : 'text-ash'"
    >
      {{ count }}
    </span>
  </RouterLink>
</template>

<script setup lang="ts">
import { RouterLink, type RouteLocationRaw } from 'vue-router'

defineProps<{ to: RouteLocationRaw; active: boolean; count?: number }>()
</script>
