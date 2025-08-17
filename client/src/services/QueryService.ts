import { QueryClient, type InvalidateQueryFilters } from "@tanstack/react-query";
import axios from "axios";

export const queryClient = new QueryClient()

export const forkliftsQueryKey = "forklifts";

export function invalidateForklifts(
    filters?: InvalidateQueryFilters
  ): Promise<void> {
    return queryClient.invalidateQueries({
      queryKey: [forkliftsQueryKey],
      ...filters
    });
  }

  export function cancelTokenFromAbort(signal?: AbortSignal) {
    const src = axios.CancelToken.source();
    if (signal) {
      signal.addEventListener("abort", () => src.cancel("aborted"), { once: true });
    }
    return src.token;
  }