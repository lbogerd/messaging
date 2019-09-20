<template>
  <v-content>
    <out-of-office></out-of-office>
  </v-content>
  
</template>

<script>
import OutOfOffice from '@/components/OutOfOffice.vue'

export default {
  components: {
    OutOfOffice
  },
  computed: {
    currentUser() {
      return this.$store.state.currentUser
    }
  }
}
</script>
