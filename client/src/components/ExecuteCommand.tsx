import { useEffect, useRef, useState } from "react";
import { createPortal } from "react-dom";
import { useExecuteCommand } from "../services/ForkliftsService";
import { Action as ApiAction, ActionTypes } from "../services/clients/BackendClient";
import "./ExecuteCommand.css";

type Props = {
  buttonLabel?: string;
  buttonClassName?: string;
};

export default function ExecuteCommand({
  buttonLabel = "Execute Command",
  buttonClassName = "btn btn-warning",
}: Props) {
  const [open, setOpen] = useState(false);
  const [cmd, setCmd] = useState("");
  const inputRef = useRef<HTMLInputElement | null>(null);

  const { mutate, data: res, isPending, reset } = useExecuteCommand();

  useEffect(() => {
    if (!open) return;
    const onKey = (e: KeyboardEvent) => { if (e.key === "Escape") setOpen(false); };
    document.body.classList.add("ecm-lock-scroll");
    setTimeout(() => inputRef.current?.focus(), 0);
    window.addEventListener("keydown", onKey);
    return () => {
      window.removeEventListener("keydown", onKey);
      document.body.classList.remove("ecm-lock-scroll");
    };
  }, [open]);

  function actionToText(a: ApiAction): string {
    switch (a.direction) {
      case ActionTypes.Forward:  return `Move Forward by ${a.value} metres.`;
      case ActionTypes.Backward: return `Move Backward by ${a.value} metres.`;
      case ActionTypes.Left:     return `Turn Left by ${a.value} degrees.`;
      case ActionTypes.Right:    return `Turn Right by ${a.value} degrees.`;
      default:                   return `Unknown action ${String(a.direction)} ${a.value}`;
    }
  }

  const hasBusinessError = Boolean(res?.error?.trim());
  const hasActions = Boolean(res?.actions?.length);

  const modal = (
    <div
      className="ecm-modal"
      role="dialog"
      aria-modal="true"
      aria-labelledby="cmd-title"
      onClick={(e) => { if (e.target === e.currentTarget) setOpen(false); }}
    >
      <div className="ecm-modal-card">
        <div className="ecm-modal-header">
          <h2 id="cmd-title">Execute Command</h2>
          <button className="ecm-icon-btn" onClick={() => setOpen(false)} aria-label="Close">×</button>
        </div>

        <div className="ecm-modal-body">
          <label className="ecm-field">
            <span>Command</span>
            <input
              ref={inputRef}
              value={cmd}
              onChange={(e) => setCmd(e.target.value)}
              placeholder="F10R90L90B5"
              aria-label="Forklift command"
            />
          </label>

          <div className="ecm-modal-actions">
            <button className="btn btn-secondary" onClick={() => setOpen(false)} disabled={isPending}>
              Close
            </button>
            <button
              className="btn btn-primary"
              onClick={() => { const v = cmd.trim(); if (v) mutate({ command: v }); }}
              disabled={isPending || !cmd.trim()}
            >
              {isPending ? "Executing..." : "Run"}
            </button>
          </div>

          <div className="ecm-log" aria-live="polite">
            {isPending && <p className="muted">Executing...</p>}
            {hasBusinessError && <p className="error">{res!.error}</p>}
            {!hasBusinessError && hasActions && res!.actions!.map((a, i) => (
              <p key={i}>{actionToText(a as ApiAction)}</p>
            ))}
            {!isPending && !hasBusinessError && !hasActions && <p className="muted">No actions.</p>}
          </div>
        </div>
      </div>
    </div>
  );

  return (
    <>
      <button className={buttonClassName} onClick={() => { reset(); setCmd(""); setOpen(true); }}>
        {buttonLabel}
      </button>
      {open && createPortal(modal, document.body)}
    </>
  );
}