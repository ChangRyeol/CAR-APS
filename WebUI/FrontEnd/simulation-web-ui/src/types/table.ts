export interface TableColumn {
  name: string
  label: string
  field: string
}

export interface TableData {
  id: number
  [key: string]: any
}

export interface TableConfig {
  label: string
  columns: TableColumn[]
  data: TableData[]
}
