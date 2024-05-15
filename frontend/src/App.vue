<script setup lang="ts">
import { provideApolloClient, useQuery } from "@vue/apollo-composable";
import { apolloClient } from "./helpers/AplloClient";
import gql from "graphql-tag";
import { computed } from "vue";
// import GetCompany from './query/GetCompany.gql'

const getCompaniesQuery = gql`
  query GetCompanies {
    companies {
      id
      inn
      name
      status
      establishmentDate
    }
  }
`;

const getProductsQuery = gql`
  query GetProducts {
    products {
      code
      description
      id
      status
      subProducts {
        id
        description
        code
        status
      }
    }
  }
`;

const query = provideApolloClient(apolloClient)(() =>
  useQuery(getProductsQuery)
);
const companies = computed(() => query.result.value?.products);

console.log("Welcome, ", companies.value);
</script>

<template>
  <div>
    <h1>Welcome to GraphQL</h1>
    <div v-for="company in companies" :key="company.id">
      <div>Name: {{ company.code }}</div>
      <div>Description: {{ company.description }}</div>
      <div>Status: {{ company.status}}</div>
      <div v-for="subProducts in company.subProducts" :key="subProducts.id">
        {{ subProducts }}
      </div>
    </div>
  </div>
</template>

<style scoped></style>
