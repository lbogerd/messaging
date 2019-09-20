<template>
  <div class="main">
    <v-content>
      <v-container 
        v-if="isLoading"
        bg
        fill-height
        grid-list-md
        text-xs-center
      >
        <v-layout 
          row
          wrap
          align-center 
        >
          <v-flex>
            <v-progress-circular 
              indeterminate 
              color="primary"
              id="main-spinner"
            ></v-progress-circular>
          </v-flex>
        </v-layout>
      </v-container>
      <v-list 
        two-line 
        v-else-if="!isLoading"
      >
        <v-list-tile 
          v-for="convo in conversationList"
          :key="convo.id"
          @click ="openConversation(convo.id)"
        >
          <v-list-tile-content>
            <v-list-tile-title>
              <span class="font-weight-bold">{{ convo.title }}</span>
              <span class="grey--text text--darken-1 font-weight-light font-italic">&nbsp;{{ convo.internalReference }}</span>
            </v-list-tile-title>
            <v-list-tile-sub-title>
              {{ getLastMessage(convo.messages).sentBy.firstName }}&nbsp;{{ getLastMessage(convo.messages).sentBy.lastName }}
              &nbsp;&mdash;&nbsp;
              <span class="grey--text">{{ getLastMessage(convo.messages).body }}</span>
            </v-list-tile-sub-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
      <!-- <v-btn
        fab
        dark
        fixed
        color="orange"
        bottom
        right
        to="/conversation/new"
      >
        <v-icon>add</v-icon>
      </v-btn> -->
      <v-expand-transition>
        <router-view
        ></router-view>
      </v-expand-transition>
    </v-content>
  </div>
  
</template>
<script>
  export default {
    data: () => ({
      dialog: false
    }),
    mounted: function () {
      this.$store.dispatch('getConversations')
    },
    computed: {
      conversationList() {
        return this.$store.state.conversations
      },
      isLoading() {
        return this.$store.state.loading
      }
    },
    methods: {
      getLastMessage: function (messages) {
        return messages[messages.length - 1];
      },
      openConversation: function (conversationId) {
        if (conversationId === this.$route.params.id) {
          this.$router.push('/')
        } else {
          this.$router.push({ path: `/conversation/${conversationId}` })
        }
      }
    }
  }
</script>
<style scoped>
  #main-spinner{
    margin-top: 5em;
  }
</style>
