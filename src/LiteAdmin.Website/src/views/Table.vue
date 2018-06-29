<template>
    <div class="home">
        <h1>{{tableName}}</h1>

        <table>
            <thead>
                <tr>
                    <td v-for="column in tableSchema.columns">{{column.name}}</td>
                </tr>
            </thead>
            <tbody>
                <tr v-for="item in items">
                    <td v-for="column in tableSchema.columns" :key="column.name">{{item[column.name]}}</td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="ts" src="./Table.ts"></script>
