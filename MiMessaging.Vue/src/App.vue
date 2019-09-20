<template>
  <v-app id="ms">
    <v-navigation-drawer
      v-model="drawer"
      fixed
      clipped
      app
      class="grey lighten-4"
    >
      <v-list
        dense
      >
        <template v-for="(item, i) in items">
          <v-layout
            v-if="item.heading"
            :key="i"
            row
            align-center
          >
            <v-subheader v-if="item.heading">
              {{ item.heading }}
            </v-subheader>
          </v-layout>
          <v-divider
            v-else-if="item.divider"
            :key="i"
            dark
            class="my-3"
          ></v-divider>
          <v-divider
            v-else-if="item.subdivider"
            :key="i"
            dark
          ></v-divider>
          <v-list-tile
            v-else
            :key="i"
            :to="item.url"
          >
            <v-list-tile-action>
              <v-icon>{{ item.icon }}</v-icon>
            </v-list-tile-action>
            <v-list-tile-content>
              <v-list-tile-title>
                {{ item.text }}
              </v-list-tile-title>
            </v-list-tile-content>
          </v-list-tile>
        </template>
      </v-list>
    </v-navigation-drawer>
    <v-toolbar 
      dark 
      color="primary" 
      app  
      clipped-left
    >
      <v-toolbar-side-icon @click="drawer = !drawer"></v-toolbar-side-icon>
      <router-link 
        to="/" 
        tag="a"
      >
        <img 
          src="./assets/logo.png"
          class="ml-3"
          height="35"
          width="35"
        />
        <span id="appTitle" class="title ml-3 hidden-sm-and-down">Medische inbox</span>
      </router-link>
      <v-text-field
        color="orange"
        solo-inverted
        flat
        hide-details
        label="Zoeken"
        prepend-inner-icon="search"
        class="ml-5"
      ></v-text-field>
      <v-spacer></v-spacer>
      <v-toolbar-items>
        <v-btn 
          flat
          :to="'/user/' + currentUser.id"
        >
          <v-icon class="mr-2">person</v-icon>
          <span class="hidden-xs-only">
            {{ currentUser.firstName }} {{ currentUser.lastName }}
          </span>
        </v-btn>
      </v-toolbar-items>
      
    </v-toolbar>
    <router-view></router-view>
  </v-app>
</template>

<script>
export default {
  data: () => ({
      drawer: null,
      items: [
        { icon: 'add', text: 'Nieuw vraag stellen', url: '/conversation/new' },
        { divider: true },
        { heading: 'Postvakken' },
        { icon: 'announcement', text: 'Nieuwe vragen' },
        { icon: 'question_answer', text: 'In behandeling'},
        { icon: 'archive', text: 'Archief' },
        { subdivider: true },
        { icon: 'all_inbox', text: 'Alle vragen'},
      ]
  }),
  computed: {
    currentUser() {
      return this.$store.state.currentUser
    }
  }
}    
</script>

<style>
#appTitle{
  color: white;
  text-decoration: none !important;
}
</style>