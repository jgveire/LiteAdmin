<template>
    <div class="home">
        <h1>{{getFriendlyName(tableName)}}</h1>

        <table>
            <thead>
                <tr>
                    <th v-for="(column, index) in tableSchema.columns"
                        v-if="index < 5 && !column.isPrimaryKey">
                        {{getFriendlyName(column.name)}}
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(item, index) in items" v-on:click="showDetails(item)">
                    <td v-for="column in tableSchema.columns" 
                        :key="column.name" 
                        v-if="index < 5 && !column.isPrimaryKey">
                        {{item[column.name]}}
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="ts" src="./Table.ts"></script>
