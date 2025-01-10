<script setup>
import {ref} from 'vue'
</script>

<script>
import axios from 'axios';

export default {
    props:{
        userId: {
            type: Number,
            required: true
        }
    },
    name: 'InvestmentList',
    data() {
        return {
            investments: []
        };
        
    },
    watch: {
        userId(newUserId) {
            this.fetchInvestments(newUserId)
        }

    },
    methods: {
        async fetchInvestments(userId) {
            const apiUrl = import.meta.env.VITE_API_URL
            console.log(apiUrl);
            try{
                const apiResponse = await axios.get(apiUrl + '/api/investmentdata/' + userId);
                this.investments = apiResponse.data;
                console.log(this.investments);
            }
            catch(error){
                console.log("There was an issue finding investment data: " + error);
            }
        }
    }
};
</script>

<template>
    <h3>Investments: </h3>
    <div class=" overflow-x-auto">
        <table  v-if="investments.length > 0" class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
            <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                    <tr>
                    <th scope="col" class="px-6 py-3">Company</th>
                    <th scope="col" class="px-6 py-3">Total Shares</th>
                    <th scope="col" class="px-6 py-3">Cost Basis Per Share</th>
                    <th scope="col" class="px-6 py-3">Current Value</th>
                    <th scope="col" class="px-6 py-3">Current Price</th>
                    <th scope="col" class="px-6 py-3">Term</th>
                    <th scope="col" class="px-6 py-3">Total Gain/Lost</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="inv in investments" :key="investments.id" class="bg-white border-b dark:bg-gray-800 dark:border-gray-700">
                    <td scope="col" class="px-6 py-3">{{inv.company}}</td>
                    <td scope="col" class="px-6 py-3">{{inv.numberOfShares}}</td>
                    <td scope="col" class="px-6 py-3">{{inv.costBasisPerShare}}</td>
                    <td scope="col" class="px-6 py-3">{{inv.currentValue}}</td>
                    <td scope="col" class="px-6 py-3">{{inv.currentPrice}}</td>
                    <td scope="col" class="px-6 py-3">{{inv.term}}</td>
                    <td scope="col" class="px-6 py-3">{{inv.totalValue}}</td>
                </tr>
            </tbody>
        </table>
    <p v-else-if="userId===0 || userId===null" class="pl-2">
        Please select a user to view their investment data.
    </p>
    <p v-else class="pl-2">
        No Investments Found For This User.
    </p>
</div>
</template>