import { useEffect, useRef, useState } from "react";
import type { FileParameter } from "../services/clients/BackendClient";
import "./ForkliftsPage.css";
import { useImportForklifts, useInfiniteForklifts } from "../services/ForkliftsService";
import ExecuteCommand from "../components/ExecuteCommand";

export default function ForkliftsPage() {
  const take = 20;
  const {
    data,
    status,
    isFetching,
    fetchNextPage,
    hasNextPage,
    isFetchingNextPage,
  } = useInfiniteForklifts(take);

  const items = data?.pages.flat() ?? [];

  const sentinelRef = useRef<HTMLDivElement | null>(null);
  useEffect(() => {
    const el = sentinelRef.current;
    if (!el) return;

    const io = new IntersectionObserver((entries) => {
      const first = entries[0];
      if (first.isIntersecting && hasNextPage && !isFetching && !isFetchingNextPage) {
        fetchNextPage();
      }
    });
    io.observe(el);
    return () => io.disconnect();
  }, [hasNextPage, isFetching, isFetchingNextPage, fetchNextPage]);

  const { mutateAsync: importAsync, isPending: importing, error: importError } = useImportForklifts();
  const fileInputRef = useRef<HTMLInputElement | null>(null);
  const [toast, setToast] = useState<string | null>(null);

  async function onFileChange(e: React.ChangeEvent<HTMLInputElement>) {
    const file = e.target.files?.[0];
    if (!file) return;
    const fp: FileParameter = { data: file, fileName: file.name };
    try {
      await importAsync({ file: fp });
      setToast("Import Done");
      setTimeout(() => setToast(null), 2500);
    } catch {
      // 
    } finally {
      e.currentTarget.value = "";
    }
  }

  if (status === "pending") 
    return <div className="page"><p className="muted">Loading...</p></div>;
  if (status === "error") return <div className="page"><p className="error">Error while loading...</p></div>;

  return (
    <div className="page">
      <header className="header">
        <h1>Forklifts</h1>
        <div className="actions">
          <input
            ref={fileInputRef}
            type="file"
            accept=".csv,.json"
            onChange={onFileChange}
            aria-label="Select file to import"
            style={{ display: "none" }}
          />
          <button className="btn btn-primary" onClick={() => fileInputRef.current?.click()} disabled={importing}>
            {importing ? "Importing" : "Import"}
          </button>
        </div>
      </header>

      <div className="toolbar">
        <ExecuteCommand />
      </div>
      
      {importError 
        && 
        <div className="alert error">
            Cannot import, please try again.
        </div>}

      {isFetching && 
        !isFetchingNextPage 
        && 
        <div className="alert info">
          Updating…
        </div>}
      {toast 
        && 
        <div className="toast">
          {toast}
        </div>}

      <div className="card">
        <table className="table" role="table" aria-label="Tabla de forklifts">
          <thead>
            <tr>
              <th scope="col">Id</th>
              <th scope="col">Name</th>
              <th scope="col">Model</th>
              <th scope="col">Manufacturing Date</th>
            </tr>
          </thead>
          <tbody>
            {items.length === 0 ? (
              <tr>
                <td colSpan={4} className="empty">No Data</td>
              </tr>
            ) : (
              items.map((f) => (
                <tr key={f.id}>
                  <td>{f.id}</td>
                  <td>{f.name}</td>
                  <td>{f.modelNumber}</td>
                  <td>{f.manufacturingDate ? new Date(f.manufacturingDate).toLocaleDateString() : ""}</td>
                </tr>
              ))
            )}

            {isFetchingNextPage &&
              Array.from({ length: 3 }).map((_, i) => (
                <tr key={`skeleton-${i}`} className="skeleton-row">
                  <td><div className="skeleton" /></td>
                  <td><div className="skeleton" /></td>
                  <td><div className="skeleton" /></td>
                  <td><div className="skeleton" /></td>
                </tr>
              ))}
          </tbody>
        </table>
      </div>

      <div ref={sentinelRef} style={{ height: 1 }} />
    </div>
  );
}