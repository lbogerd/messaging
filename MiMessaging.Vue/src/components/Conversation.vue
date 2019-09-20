<template>
  <v-card
    color="blue lighten-5"
    flat
  >
    <v-container 
      id="messagesList"
      grid-list-md
    >
      <v-layout row wrap>
        <conversation-message 
          v-for="message in conversation.messages"
          :key="message.id"
          :message="message"
        ></conversation-message>
        <v-expand-transition>
          <v-flex xs12
            id="replyFieldFlex"
            v-show="replyFieldVisible"
          >
            <v-textarea
              id="replyField"
              label="Reactie"
              hide-details
              solo
              v-model="replyBody"
            ></v-textarea>
          </v-flex>
        </v-expand-transition>
      </v-layout>
    </v-container>

    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn 
        flat 
        v-show="replyFieldVisible"
        color="grey"
        @click="replyFieldVisible = !replyFieldVisible"
      >
        Annuleren
      </v-btn>
      <v-btn 
        dark
        color="orange"
        @click="addReply(replyBody)"
      >
        {{ replyAction }}
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import ConversationMessage from './ConversationMessage.vue'

export default {
  data: () => ({
    replyFieldVisible: false,
    replyBody: ''
  }),
  components: {
    ConversationMessage
  },
  computed: {
    conversation() {
      return this.$store.state.conversations.find(convo => convo.id === this.$route.params.id)
    },
    replyAction() {
      return this.replyFieldVisible
        ? 'Versturen'
        : 'Reageren'
    },
    currentUser() {
      return this.$store.state.currentUser
    }
  },
  methods: 
  {
    addReply: function(messageBody) {
      if (!this.replyFieldVisible)
      {
        this.replyFieldVisible = !this.replyFieldVisible
      }
      else
      {
        var newMessage = 
        {
          'conversationId': this.$route.params.id,
          'body': messageBody,
          // TODO: To ISO format
          'sentOn': 'nu',
          'sentBy': this.currentUser
        }
        this.$store.commit('addMessage', newMessage)
        this.replyBody = ''
        this.replyFieldVisible = !this.replyFieldVisible
      }
    }
  }
}
</script>

<style scoped>
#messagesList{
  padding-top: 1rem;
  padding-bottom: 1rem;
}

#replyFieldFlex{
  padding-top: 1rem;
}
</style>
