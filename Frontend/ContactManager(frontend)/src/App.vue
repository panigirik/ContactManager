<template>
  <div class="container">
    <h1>Contact Manager</h1>
    <ContactSearch />
    <hr />
    <ContactList />
  </div>
</template>

<script setup>
import ContactSearch from './components/ContactSearch.vue'
import ContactList from './components/ContactList.vue'
</script>