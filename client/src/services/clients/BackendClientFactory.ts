import axios from "axios";
import { BackendClient } from "./BackendClient";

const axiosInstance = axios.create({});
const BASE_URL = import.meta.env.VITE_API_BASE_URL as string;

const backendClient = new BackendClient(
    BASE_URL,
    axiosInstance
  );

  class BackendClientFactory {
    create(): BackendClient {
      return backendClient;
    }
  }
  
  export const backendClientFactory: BackendClientFactory =
    new BackendClientFactory();