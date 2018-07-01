<template>
    <div class="home">
        <h1>{{getFriendlyName(tableName)}}</h1>

        <table>
            <thead>
                <tr>
                    <th v-for="(column, index) in tableSchema.columns"
                        v-if="index < 5 && !column.isPrimaryKey"
                        :key="column.name">
                        {{getFriendlyName(column.name)}}
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="item in items"
                    v-on:click="edit(item)">
                    <td v-for="(column, index) in tableSchema.columns" 
                        v-if="index < 5 && !column.isPrimaryKey"
                        :key="column.name"                         >
                        {{item[column.name]}}
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="ts" src="./Table.ts"></script>
