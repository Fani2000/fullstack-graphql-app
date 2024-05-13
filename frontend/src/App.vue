<script setup lang="ts">
import { provideApolloClient, useQuery } from "@vue/apollo-composable";
import { apolloClient } from "./helpers/AplloClient";
import gql from "graphql-tag";
import { computed } from "vue";

const query = provideApolloClient(apolloClient)(() =>
  useQuery(gql`
    query GetCompanies{
      companies {
        id
        inn
        name
        status
        establishmentDate
      }
    }
  `)
);
const companies = computed(() => query.result.value?.companies);

console.log("Welcome, ", companies.value)
</script>

<template>
  <div>
    <h1>Welcome to GraphQL</h1>
    <div v-for="company in companies" :key="company.id">
      <div>Name: {{company.name}}</div>
      <div>Status: {{company.status}}</div>
    </div>
  </div>
</template>

<style scoped>
</style>
