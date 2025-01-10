<script setup>
  import Users from './components/Users.vue'
  import Investments from './components/Investments.vue'
  import Navbar from './components/Navbar.vue'
</script>
<template>
  <div class="flex flex-col h-screen w-screen px-16">
    <Navbar class="flex-0"/>
    <Users class="flex- max-w-lg" @selected-userid="handleUserChange" />
    <Investments class="flex-0" :userId="selectedUserId"/>
  </div>

</template>

<script>

export default {
  components: {
    Users,
    Investments
  },
  data() {
    return {
      selectedUserId: 0
    };
  },
  methods: {
    handleUserChange(userId) {
      this.selectedUserId = userId
    }
  }
  
};

</script>
