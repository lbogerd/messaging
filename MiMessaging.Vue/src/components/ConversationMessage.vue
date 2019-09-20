<template>
  <v-flex 
    v-if="message.sentBy.id === currentUserId"
    xs11
    offset-xs1
  >
    <v-card
      color="accent"
    >
      <v-card-title primary-title>
        {{ message.body }}
      </v-card-title>
    </v-card>
  </v-flex>
  <v-flex 
    v-else
    xs11
  >
    <v-card
      class="lighten-2"
    >
      <v-card-title primary-title>
        {{ message.body }}
      </v-card-title>
    </v-card>
  </v-flex>
</template>

<script>
export default {
  props: {
    message: Object
  },
  computed: {
    currentUserId() {
      return this.$store.state.currentUser.id
    }
  }
}
</script>
