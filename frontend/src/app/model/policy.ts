import { Agent } from './agent';

export interface policy {
  id: number;
  PlanName: string;
  ProviderName: string;
  AgentName: string;
  ClientName: string;
  CoverageAmount: number;
  Frequency: string;
  TimePeriod: number;
  Premium: number;
  Commision: number;
  EnrolledOn: string;
  CancelledOn?: string | null;
  ExpiredOn?: string | null;

  // id: string;
  // CommisionAmount: any;
  // Provider: string;
  // Client: string;
  // Commision: number;
  // Coverage: number;
  // StartDate: string;
  // Premium: number;
  // Agent: Agent;
}
