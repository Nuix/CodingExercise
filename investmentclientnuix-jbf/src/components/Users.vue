<script setup>
import {ref} from 'vue'
</script>

<script>
import axios from 'axios';

export default {
    name: 'Users',
    data() {
        console.log("initializing data")
        return {
            users: []
        };
        
    },
    mounted() {
        this.fetchUsers();
    },
    methods: {
        async fetchUsers() {
            const apiUrl = import.meta.env.VITE_API_URL
            try{
                const apiResponse = await axios.get(apiUrl + '/api/userdata');
                this.users = apiResponse.data;
                console.log(this.users)
            }
            catch(error){
                console.log("There was an issue finding user data: " + error);
            }
        },
        onChange(event){
            this.$emit('selected-userid', parseInt(event.target.value));
        }
    }
};
</script>

<template>
    <div>Please Select a User To View Their Investments: 
        <select @change="onChange" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500">
            <option value=0>...</option>
            <option v-for="user in users" :key="user.id" :value="user.id">
                {{user.lastName}}, {{ user.firstName }}
            </option>
        </select>
    </div>
</template>