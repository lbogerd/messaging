<template>
  <div class="text-xs-center">
    <v-dialog
      v-model="dialog"
      width="650"
    >
      <v-card>
        <v-card-title
          class="headline primary white--text"
          primary-title
        >
          Nieuwe vraag
        </v-card-title>

        <v-card-actions>
          <v-btn 
            color="primary"
            flat
          >
            Foto toevoegen
          </v-btn>
          <v-btn 
            color="primary"
            flat
          >
            Link invoegen
          </v-btn>
        </v-card-actions>

        <v-card-text
          id="newQuestion"
        >
          <v-text-field
            name="name"
            label="Onderwerp"
            id="newQuestionSubject"
            v-model="question.subject"
          ></v-text-field>

          <v-textarea
            id="newQuestionBody"
            hide-details
            label="Inhoud"
            v-model="question.body"
          ></v-textarea>
        </v-card-text>

        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn 
            flat
            color="grey"
            @click="dialog = false"
          >
            Annuleren
          </v-btn>
          <v-btn
            dark
            color="orange"
            @click="newConversation(question)"
          >
            Vraag versturen
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
  export default {
    data: () => ({
      dialog: true,
      question: 
      {
        subject: '',
        body: ''
      }
    }),
    computed: {
      currentUser() {
        return this.$store.state.currentUser
      }
    },
    methods: {
      newConversation: function(newQuestion){
        var newConversation = {
          'id': '',
          'title': newQuestion.subject,
          'internalReference': 'salesforceid',
          'startedOn': '',
          'currentStatus': '',
          'messages': 
          [
            {
              'id': '',
              'conversationId': '',
              'sentOn': '',
              'body': newQuestion.body,
              'sentBy': this.currentUser.id
            }
          ],
          'participants': []
        }

        this.$store.commit('addConversation', newConversation)
        this.question = {}
        this.dialog = false
      }
    },
    watch: {
      dialog: function (newValue){
        if(!newValue)
          this.$router.push('/');
      }
    }
  }
</script>

<style scoped>
  #newQuestion{
    padding-top: 0;
  }
</style>
