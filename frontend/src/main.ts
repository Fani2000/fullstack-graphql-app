import { createApp, provide, h } from "vue";
import "./style.css";
import App from "./App.vue";
import { DefaultApolloClient } from "@vue/apollo-composable";
import { apolloClient } from "./helpers/AplloClient";

const app = createApp({
  setup() {
    provide(DefaultApolloClient, apolloClient);
  },

  render: () => h(App),
});

app.mount("#app")