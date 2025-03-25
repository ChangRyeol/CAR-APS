<template>
  <q-page class="q-pa-md">
    <q-card>
      <q-card-section>
        <div class="text-h5">디스패치 로그</div>
      </q-card-section>

      <q-separator />
      <q-card-section>
        <q-btn color="primary" label="디스패치 로그 불러오기" @click="loadDispatchLog" />
        <q-table
          v-if="dispatch_log.length > 0"
          :rows="dispatch_log"
          :columns="columns"
          dense
          bordered
          class="q-mt-md"
        />
      </q-card-section>
    </q-card>
  </q-page>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import axios from 'axios'
import api from '../api/axiosInstance'
const dispatch_log = ref<
  Array<{
    simulation_version: string
    eqp_id: string
    step_id: string
    dispatching_time: Date
    lot_id: string
    candidate_lots: string
    passed_lots: string
    exclude_lots: string
    dispatch_info: string
  }>
>([])
const columns = ref([
  { name: 'SIMULATION_VERSION', required: true, label: '버전', field: 'SIMULATION_VERSION' },
  { name: 'EQP_ID', required: true, label: '프로세스 ID', field: 'EQP_ID' },
  { name: 'STEP_ID', required: true, label: '공정 ID', field: 'STEP_ID' },
  { name: 'DISPATCHING_TIME', required: true, label: '디스패치 시간간', field: 'DISPATCHING_TIME' },
  { name: 'LOT_ID', required: true, label: 'LOT ID', field: 'LOT_ID' },
  { name: 'CANDIDATE_LOTS', required: true, label: '디스패치 후보 LOT', field: 'CANDIDATE_LOTS' },
  { name: 'PASSED_LOTS', required: true, label: '통과 LOT', field: 'PASSED_LOTS' },
  { name: 'EXCLUDE_LOTS', required: true, label: '제외 LOT', field: 'EXCLUDE_LOTS' },
  { name: 'DISPATCH_INFO', required: true, label: '디스패치 정보보', field: 'DISPATCH_INFO' },
])

const loadDispatchLog = async (): Promise<any> => {
  try {
    const response = await api.get('/get-dispatch-log', {
      params: {
        version: 'VER_20250301_182331',
      },
    })
    dispatch_log.value = response.data
  } catch (error) {
    console.error('Error fetching data:', error)
    throw error
  }
}
</script>
