import { useInfiniteQuery, useMutation } from "@tanstack/react-query";
import { backendClientFactory } from "./clients/BackendClientFactory";
import type { CommandExecuteResponse, FileParameter, GetForkliftResponse } from "./clients/BackendClient";
import { forkliftsQueryKey, invalidateForklifts } from "./QueryService";

  async function getForklifts(
    skip?: number | undefined,
    take?: number | undefined
  ): Promise<GetForkliftResponse[]> {
    const backendClient = backendClientFactory.create();
    const forklifts = await backendClient.forklift(skip, take);
  
    return forklifts;
  }

  export function useInfiniteForklifts(take?: number | undefined) {
    const t = take ?? 20;
  
    return useInfiniteQuery({
      queryKey: [forkliftsQueryKey, "infinite", t],
      queryFn: ({ pageParam = 0 }) => getForklifts(pageParam as number, t),
      initialPageParam: 0,
      getNextPageParam: (lastPage, _allPages, lastPageParam) =>
        lastPage.length === t ? (lastPageParam as number) + t : undefined
    });
  }

  async function importForklifts(file: FileParameter) {
    const backendClient = backendClientFactory.create();
    await backendClient.import(file);
  
    await invalidateForklifts();
  }
  
  export function useImportForklifts() {
    return useMutation({
      mutationFn: ({ file }: { file: FileParameter }) => importForklifts(file)
    });
  }

  async function executeCommand(command: string): Promise<CommandExecuteResponse> {
    const backendClient = backendClientFactory.create();
    return await backendClient.command(command);
  }
  
  export function useExecuteCommand() {
    return useMutation({
      mutationFn: ({ command }: { command: string }) => executeCommand(command),
    });
  }