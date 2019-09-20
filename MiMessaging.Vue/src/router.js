import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: '',
      component: Home,
      children: [
        {
          path: '/conversation/new',
          name: 'new',
          component: () => import(/* webpackChunkName: "newconversation" */ './components/NewConversation.vue')
        },
        {
          path: '/conversation/:id',
          name: 'conversation',
          component: () => import(/* webpackChunkName: "conversation" */ './components/Conversation.vue')
        }
       ]
    },
    {
      path: '/user/:id',
      name: 'user',
      component: () => import(/* webpackChunkName: "user" */ './views/User.vue')
    }
  ]
})
