import Vue from 'vue'
import Vuex from 'vuex'
import Axios from 'axios'

Vue.use(Vuex);

// const apiUrl = 'https://mivueapi.azurewebsites.net/api/';
const apiUrl = 'https://localhost:5001/api/';
// const headers = 
//   {
//     'Content-Type': 'application/json'
//   }

export const store = new Vuex.Store({
  state: {
    conversations: [],
    loading: false,
    currentUser: {
      "id": "7fc5c7c1-864b-498d-93c7-08d676ea4453",
      "firstName": "Handler",
      "lastName": "User",
      "outOfOffice": null
    }
  },
  mutations: {
    setConversations: (state, payload) => {
      state.conversations = payload
      state.loading = false
    },
    addConversation: (state, payload) => {
      Axios.post(apiUrl + 'conversations', payload)
        .then(function () 
        {
          state
            .conversations
            .push(payload)
          
          Vue.set(state.conversations) 
        })
        // eslint-disable-next-line no-console
        .catch(function (error) {console.log(error)})         
    },
    setLoading: (state, payload) => {
      state.loading = payload
    },
    addMessage: (state, payload) => {
      // Assign random GUID, see https://stackoverflow.com/a/22856022
      for (var i=0; i<32; i++)
      {
        payload.id+=Math.floor(Math.random()*0xF).toString(0xF);
      }
      
      state
        .conversations
        .find(m => m.id === payload.conversationId)
        .messages
        .push(payload)

      Vue.set(state.conversations)

      // state
      //   .conversations
      //   .find(m => m.id === payload.conversationId)
      //   .messages
      //   .push(payload)
    }
  },
  actions: {
    getConversations: async (context) => {
      context.commit('setLoading', true)
      
      Axios.get(apiUrl + 'conversations')
        .then(function (response) 
        {
          context.commit('setConversations', response.data)      
        })
    }
  }
})